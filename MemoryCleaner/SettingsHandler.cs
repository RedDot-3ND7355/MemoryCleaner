using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Forms;

namespace MemoryCleaner
{
    public static class SettingsHandler
    {
        private static readonly string SettingsDirectory =
            Path.GetDirectoryName(Application.ExecutablePath) ?? AppContext.BaseDirectory;

        private static readonly string JsonPath =
            Path.Combine(SettingsDirectory, "Settings.json");

        private static readonly string LegacyConfPath =
            Path.Combine(SettingsDirectory, "Settings.conf");

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            ReadCommentHandling = JsonCommentHandling.Skip,
            AllowTrailingCommas = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.Never
        };

        public static AppSettings Current { get; private set; } = new();

        // Initialize Settings
        public static void IniSettings()
        {
            if (File.Exists(JsonPath))
            {
                Current = ReadJsonOrDefault();
                return;
            }

            // Migrate legacy Settings.conf if present
            if (File.Exists(LegacyConfPath))
            {
                Current = LoadFromLegacyConf(LegacyConfPath);
                WriteJson(Current);

                try
                {
                    File.Delete(LegacyConfPath);
                }
                catch
                {
                    // Keep legacy file if delete fails; JSON is source of truth now
                }

                return;
            }

            Current = new AppSettings();
            WriteJson(Current);
        }

        // Save Overlay Settings
        public static void SaveOverlaySettings(bool enabled, string position)
        {
            Current.OverlayEnabled = enabled;
            Current.OverlayPosition = position;
            WriteJson(Current);
        }

        // Get Overlay Position
        public static OverlayPosition GetOverlayPosition()
        {
            return Enum.TryParse(Current.OverlayPosition, true, out OverlayPosition pos)
                ? pos
                : OverlayPosition.TopRight;
        }

        // Save Settings
        public static void SaveSettings()
        {
            if (NewDesign.CurrentForm == null) return;

            Current = new AppSettings
            {
                Interval = NewDesign.CurrentForm.intervalComboBox.SelectedItem?.ToString() ?? "OFF",
                Startup = NewDesign.CurrentForm.startonbootCheckBox.Checked,
                Advanced = NewDesign.CurrentForm.advancedlogsCheckBox.Checked,
                Cached = NewDesign.CurrentForm.clearstandbymemCheckBox.Checked,
                StartMinimized = NewDesign.CurrentForm.startminimizedCheckBox.Checked,
                // keep overlay values already on Current (saved from tray)
                OverlayEnabled = Current.OverlayEnabled,
                OverlayPosition = Current.OverlayPosition
            };

            WriteJson(Current);
        }

        // Read Settings
        public static void ReadSettings()
        {
            if (NewDesign.CurrentForm == null) return;

            Current = ReadJsonOrDefault();

            // Apply to UI (SelectedItem only updates when the value exists in the combo box)
            var form = NewDesign.CurrentForm;

            if (form.intervalComboBox.Items.Contains(Current.Interval))
                form.intervalComboBox.SelectedItem = Current.Interval;
            else
                form.intervalComboBox.SelectedItem = "OFF";

            form.startonbootCheckBox.Checked = Current.Startup;
            form.advancedlogsCheckBox.Checked = Current.Advanced;
            form.clearstandbymemCheckBox.Checked = Current.Cached;
            form.startminimizedCheckBox.Checked = Current.StartMinimized;
        }

        private static AppSettings ReadJsonOrDefault()
        {
            try
            {
                if (!File.Exists(JsonPath))
                    return new AppSettings();

                string json = File.ReadAllText(JsonPath);
                return JsonSerializer.Deserialize<AppSettings>(json, JsonOptions) ?? new AppSettings();
            }
            catch
            {
                return new AppSettings();
            }
        }

        private static void WriteJson(AppSettings settings)
        {
            string json = JsonSerializer.Serialize(settings, JsonOptions);
            File.WriteAllText(JsonPath, json);
        }

        private static AppSettings LoadFromLegacyConf(string path)
        {
            var settings = new AppSettings();

            try
            {
                foreach (string line in File.ReadAllLines(path))
                {
                    if (TryGetValue(line, "Interval = ", out string interval))
                        settings.Interval = interval;
                    else if (TryGetValue(line, "Startup = ", out string startup) && bool.TryParse(startup, out bool startupValue))
                        settings.Startup = startupValue;
                    else if (TryGetValue(line, "Advanced = ", out string advanced) && bool.TryParse(advanced, out bool advancedValue))
                        settings.Advanced = advancedValue;
                    else if (TryGetValue(line, "Cached = ", out string cached) && bool.TryParse(cached, out bool cachedValue))
                        settings.Cached = cachedValue;
                    else if (TryGetValue(line, "SMinimized = ", out string minimized) && bool.TryParse(minimized, out bool minimizedValue))
                        settings.StartMinimized = minimizedValue;
                }
            }
            catch
            {
                return new AppSettings();
            }

            return settings;
        }
        public sealed class AppSettings
        {
            public string Interval { get; set; } = "OFF";
            public bool Startup { get; set; } = false;
            public bool Advanced { get; set; } = false;
            public bool Cached { get; set; } = true;
            public bool StartMinimized { get; set; } = false;
            public bool OverlayEnabled { get; set; } = false;
            public string OverlayPosition { get; set; } = "TopRight";
        }

        private static bool TryGetValue(string line, string prefix, out string value)
        {
            if (line.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
            {
                value = line[prefix.Length..].Trim();
                return true;
            }

            value = string.Empty;
            return false;
        }
    }
}

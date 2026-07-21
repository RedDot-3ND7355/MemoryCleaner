using ReaLTaiizor.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
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
                return;

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

        // Save Settings
        public static void SaveSettings()
        {
            if (Form1.CurrentForm == null)
                return;

            Current = new AppSettings
            {
                Interval = Form1.CurrentForm.materialComboBox1.SelectedItem?.ToString() ?? "OFF",
                Startup = Form1.CurrentForm.materialCheckbox1.Checked,
                Advanced = Form1.CurrentForm.materialCheckbox2.Checked,
                Cached = Form1.CurrentForm.materialCheckbox3.Checked,
                StartMinimized = Form1.CurrentForm.materialCheckbox4.Checked
            };

            WriteJson(Current);
        }

        // Read Settings
        public static void ReadSettings()
        {
            if (Form1.CurrentForm == null)
                return;

            Current = ReadJsonOrDefault();

            // Apply to UI (SelectedItem only updates when the value exists in the combo box)
            if (Form1.CurrentForm.materialComboBox1.Items.Contains(Current.Interval))
                Form1.CurrentForm.materialComboBox1.SelectedItem = Current.Interval;
            else
                Form1.CurrentForm.materialComboBox1.SelectedItem = "OFF";

            Form1.CurrentForm.materialCheckbox1.Checked = Current.Startup;
            Form1.CurrentForm.materialCheckbox2.Checked = Current.Advanced;
            Form1.CurrentForm.materialCheckbox3.Checked = Current.Cached;
            Form1.CurrentForm.materialCheckbox4.Checked = Current.StartMinimized;
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

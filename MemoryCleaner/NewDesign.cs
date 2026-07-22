using IWshRuntimeLibrary;
using ReaLTaiizor.Forms;
using ReaLTaiizor.Manager;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoryCleaner
{
    public partial class NewDesign : MaterialForm
    {
        public readonly MaterialSkinManager materialSkinManager;

        public _MemoryCleaner MemoryCleaner = new _MemoryCleaner();
        private BlackListGUI blackList;
        public static NewDesign CurrentForm;

        private bool AppStarted = false;
        private bool bypass = false;
        private readonly string appver = GetAppVersion();

        private RamOverlayForm? _ramOverlay;
        private ToolStripMenuItem? _overlayToggleItem;
        private RAMcs ramInfo = new RAMcs();

        #region Constructor & Initialization

        public NewDesign()
        {
            InitializeComponent();
            CurrentForm = this;

            // Material Skin
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.EnforceBackcolorOnAllComponents = true;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;

            // Core Settings
            SettingsHandler.IniSettings();
            SettingsHandler.ReadSettings();

            // Update Form Title with Version
            this.Text = $"Very Simple Memory Cleaner | v{appver}";

            // Set App as Started
            AppStarted = true;

            // Basic core setup
            CheckCachedCleanPerm();
            CheckForExistingTimer();
            CheckForStartAsMinimized();

            // Initialize Tray Menu
            IniTrayMenu();

            // Init RAM Overlay if Enabled
            if (SettingsHandler.Current.OverlayEnabled)
                SetOverlayEnabled(true);

            // Initialize Blacklist GUI
            blackList = new BlackListGUI();

            // Reset Divider Color
            materialDivider1.BackColor = circularramProgress1.GetProgressColor(0);

            // Real-time RAM Update
            ramUpdateTimer = new System.Windows.Forms.Timer();
            ramUpdateTimer.Interval = 2000; // Update every 2 seconds
            ramUpdateTimer.Tick += RamUpdateTimer_Tick;
            ramUpdateTimer.Start();

            // Reset Launcher Log
            ResetLauncherLog();
        }

        #endregion

        #region Form Buttons

        private async void cleanButton_Click(object sender, EventArgs e)
            => await StartCleanAsync();

        private void minimizeToTrayButton_Click(object sender, EventArgs e)
            => MinimizeToTray();

        private void blacklistButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            blackList?.ShowDialog();
            this.Show();
            this.TopMost = true;
            Thread.Sleep(50);
            this.TopMost = false;
        }

        private void aboutButton_Click(object sender, EventArgs e)
        {
            string aboutText =
            $"Very Simple Memory Cleaner\r\n" +
            $"Version: {appver}\r\n" +
            $"Made by Endless (Kogaruh / RedDot-3ND7355)\r\n" +
            "A lightweight and simple RAM cleaner.\r\n" +
            "Run as Administrator for best results (especially Standby cache clearing).\r\n" +
            "Thank you for using the tool!";

            ReaLTaiizor.Controls.MaterialMessageBox.Show(
                aboutText,
                "About Very Simple Memory Cleaner",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        #endregion

        #region Checkboxes & Controls

        private void startonbootCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!AppStarted) return;
            SettingsHandler.SaveSettings();

            if (startonbootCheckBox.Checked)
            {
                try
                {
                    WshShell wshShell = new WshShell();
                    IWshShortcut shortcut = (IWshShortcut)wshShell.CreateShortcut(
                        Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\" + Application.ProductName + ".lnk");

                    shortcut.TargetPath = Application.ExecutablePath;
                    shortcut.WorkingDirectory = Application.StartupPath;
                    shortcut.Description = "Memory Cleaner Startup";
                    shortcut.Save();
                }
                catch { }
            }
            else
            {
                try
                {
                    System.IO.File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\" + Application.ProductName + ".lnk");
                }
                catch { }
            }
        }

        private void startminimizedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (AppStarted) SettingsHandler.SaveSettings();
        }

        private void advancedlogsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (AppStarted) SettingsHandler.SaveSettings();
        }

        private void clearstandbymemCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (AppStarted)
            {
                SettingsHandler.SaveSettings();
                CheckCachedCleanPerm();
            }
        }

        private void intervalComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!AppStarted) return;

            if (!bypass)
                SettingsHandler.SaveSettings();
            else
                bypass = false;

            if (intervalComboBox.SelectedItem?.ToString() != "OFF" &&
                int.TryParse(intervalComboBox.SelectedItem?.ToString(), out int minutes))
            {
                CleanerTimer.Interval = minutes * 60000;
                CleanerTimer.Start();
            }
            else
            {
                CleanerTimer.Stop();
            }
        }

        #endregion

        #region Core Cleaning & Timer

        private Task StartCleanAsync() =>
            MemoryCleaner.CleanMemAsync(advancedlogsCheckBox.Checked, clearstandbymemCheckBox.Checked);

        private async void CleanerTimer_Tick(object sender, EventArgs e)
            => await StartCleanAsync();

        #endregion

        #region Tray & Window Management

        private void IniTrayMenu()
        {
            var menu = new ContextMenuStrip();

            var cleanItem = new ToolStripMenuItem("Clean now");
            cleanItem.Click += async (_, _) => await StartCleanAsync();

            var openItem = new ToolStripMenuItem("Open");
            openItem.Click += (_, _) => RestoreFromTray();

            _overlayToggleItem = new ToolStripMenuItem("RAM overlay")
            {
                CheckOnClick = true,
                Checked = SettingsHandler.Current.OverlayEnabled
            };
            _overlayToggleItem.CheckedChanged += (_, _) => SetOverlayEnabled(_overlayToggleItem.Checked);

            var posMenu = new ToolStripMenuItem("Overlay position");
            posMenu.DropDownItems.Add(MakePosItem("Top left", OverlayPosition.TopLeft));
            posMenu.DropDownItems.Add(MakePosItem("Top right", OverlayPosition.TopRight));
            posMenu.DropDownItems.Add(MakePosItem("Bottom left", OverlayPosition.BottomLeft));
            posMenu.DropDownItems.Add(MakePosItem("Bottom right", OverlayPosition.BottomRight));

            var exitItem = new ToolStripMenuItem("Exit");
            exitItem.Click += (_, _) =>
            {
                notifyIcon1.Visible = false;
                _ramOverlay?.Dispose();
                Application.Exit();
            };

            menu.Items.Add(cleanItem);
            menu.Items.Add(openItem);
            menu.Items.Add(new ToolStripSeparator());
            menu.Items.Add(_overlayToggleItem);
            menu.Items.Add(posMenu);
            menu.Items.Add(new ToolStripSeparator());
            menu.Items.Add(exitItem);

            notifyIcon1.ContextMenuStrip = menu;
            notifyIcon1.Text = "Memory Cleaner";
            notifyIcon1.Visible = true;
        }

        private void MinimizeToTray() => Hide();

        private void RestoreFromTray()
        {
            Show();
            WindowState = FormWindowState.Normal;
            Activate();
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                RestoreFromTray();
        }

        #endregion

        #region RAM Overlay

        private RamOverlayForm EnsureOverlay()
        {
            if (_ramOverlay == null || _ramOverlay.IsDisposed)
            {
                _ramOverlay = new RamOverlayForm();
                _ramOverlay.SetPosition(SettingsHandler.GetOverlayPosition());
            }
            return _ramOverlay;
        }

        private void SetOverlayEnabled(bool enabled)
        {
            if (enabled)
                EnsureOverlay().SetEnabled(true);
            else if (_ramOverlay != null)
            {
                _ramOverlay.SetEnabled(false);
                _ramOverlay.Dispose();
                _ramOverlay = null;
            }
            SettingsHandler.SaveOverlaySettings(enabled, SettingsHandler.Current.OverlayPosition);
        }

        private ToolStripMenuItem MakePosItem(string text, OverlayPosition position)
        {
            var item = new ToolStripMenuItem(text)
            {
                Checked = SettingsHandler.GetOverlayPosition() == position
            };

            item.Click += (_, _) =>
            {
                SettingsHandler.SaveOverlaySettings(
                    SettingsHandler.Current.OverlayEnabled,
                    position.ToString());

                _ramOverlay?.SetPosition(position);

                // Refresh checkmarks
                if (notifyIcon1.ContextMenuStrip == null) return;
                foreach (ToolStripItem ti in notifyIcon1.ContextMenuStrip.Items)
                {
                    if (ti is ToolStripMenuItem { Text: "Overlay position" } parent)
                    {
                        foreach (ToolStripItem child in parent.DropDownItems)
                        {
                            if (child is ToolStripMenuItem mi)
                                mi.Checked = mi.Text == text;
                        }
                    }
                }
            };

            return item;
        }

        #endregion

        #region Helper Methods

        private void RamUpdateTimer_Tick(object? sender, EventArgs e)
        {
            if (IsDisposed) return;

            try
            {
                int usedPercent = (int)ramInfo.GetUsagePercent();

                ulong totalBytes = ramInfo.GetTotalBytes();
                ulong availBytes = ramInfo.GetAvailableBytes();

                float totalGB = totalBytes / (1024f * 1024 * 1024);
                float freeGB = availBytes / (1024f * 1024 * 1024);

                circularramProgress1.UpdateFromRAM(usedPercent, usedPercent + "%");
                materialDivider1.BackColor = circularramProgress1.GetProgressColor(usedPercent);

                lblUsedRAM.Text = $"Used: {usedPercent}% ({totalGB - freeGB:F1}/{totalGB:F1} GB)";
                lblFreeRAM.Text = $"Free: {freeGB:F1} GB";
            }
            catch { }
        }

        private static string GetAppVersion()
        {
            try
            {
                string? path = Environment.ProcessPath ?? Application.ExecutablePath;
                if (!string.IsNullOrWhiteSpace(path) && System.IO.File.Exists(path))
                {
                    string? fileVersion = FileVersionInfo.GetVersionInfo(path).FileVersion;
                    if (!string.IsNullOrWhiteSpace(fileVersion))
                        return fileVersion.Replace(".0.0", "");
                }
                Version? version = Assembly.GetExecutingAssembly().GetName().Version;
                return version?.ToString(3) ?? "0.0.0";
            }
            catch
            {
                return "0.0.0";
            }
        }

        private static bool IsAdministrator()
        {
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        private void RestartAsAdmin(string exe)
        {
            var proc = new Process
            {
                StartInfo = { FileName = exe, UseShellExecute = true, Verb = "runas" }
            };
            try
            {
                if (proc.Start())
                    Process.GetCurrentProcess().Kill();
                else
                    clearstandbymemCheckBox.Checked = false;
            }
            catch
            {
                clearstandbymemCheckBox.Checked = false;
            }
        }

        private void CheckCachedCleanPerm()
        {
            if (clearstandbymemCheckBox.Checked && !IsAdministrator())
                RestartAsAdmin(Environment.ProcessPath ?? Application.ExecutablePath);
        }

        private void CheckForStartAsMinimized()
        {
            if (startminimizedCheckBox.Checked)
            {
                _ = Task.Delay(50).ContinueWith(_ =>
                {
                    if (!IsDisposed)
                        BeginInvoke(Hide);
                });
            }
        }

        private void CheckForExistingTimer()
        {
            bypass = true;
            intervalComboBox_SelectedIndexChanged(this, null);
            bypass = false;
        }

        public void ResetLauncherLog()
        {
            if (IsDisposed) return;
            if (InvokeRequired) { BeginInvoke(ResetLauncherLog); return; }
            logTextBox.Text = "Start of log";
        }

        public void AddLauncherLog(string log)
        {
            if (IsDisposed) return;
            if (InvokeRequired)
            {
                BeginInvoke(() => AddLauncherLog(log));
                return;
            }
            // Prepend new log (newest on top)
            logTextBox.Text = log + Environment.NewLine + logTextBox.Text;

            // Optional: limit lines to avoid huge text
            if (logTextBox.Text.Split('\n').Length > 500)
            {
                var lines = logTextBox.Text.Split('\n');
                logTextBox.Text = string.Join(Environment.NewLine, lines.Take(400));
            }
        }

        #endregion

        #region Log Control

        private void logTextBox_Enter(object sender, EventArgs e)
        {
            // Place cursor at the top instead of selecting all
            logTextBox.SelectionStart = 0;
            logTextBox.SelectionLength = 0;
        }

        #endregion
    }
}
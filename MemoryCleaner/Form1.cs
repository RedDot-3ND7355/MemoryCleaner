using IWshRuntimeLibrary;
using ReaLTaiizor.Forms;
using ReaLTaiizor.Manager;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoryCleaner
{
    public partial class Form1 : MaterialForm
    {
        // Globals
        public readonly MaterialSkinManager materialSkinManager;
        private BlackListGUI blackList;
        public _MemoryCleaner MemoryCleaner = new _MemoryCleaner();
        public static Form1 CurrentForm;
        private bool AppStarted = false;
        private bool bypass = false;
        private readonly string appver = GetAppVersion();
        // Ram Overlay Globals
        private RamOverlayForm? _ramOverlay;
        private ToolStripMenuItem? _overlayToggleItem;

        private static string GetAppVersion()
        {
            try
            {
                string? path = Environment.ProcessPath
                    ?? Application.ExecutablePath;

                if (string.IsNullOrWhiteSpace(path))
                    path = Assembly.GetExecutingAssembly().Location;

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

        // Ini Form
        public Form1()
        {
            InitializeComponent();
            // Set Static Form
            CurrentForm = this;
            // Set material colors
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.EnforceBackcolorOnAllComponents = true;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            // Ini Settings
            SettingsHandler.IniSettings();
            // Read Settings
            SettingsHandler.ReadSettings();
            // App Started Boolean
            AppStarted = true;
            // Set Version
            materialLabel5.Text = "v" + appver;
            // Restart as admin if clean cache is checked
            CheckCachedCleanPerm();
            // Set Timer if any at start
            CheckForExistingTimer();
            // Init Tray Menu
            IniTrayMenu();
            // Init RAM Overlay if Enabled
            if (SettingsHandler.Current.OverlayEnabled)
                SetOverlayEnabled(true);
            // Set Minimized if config present
            CheckForStartAsMinimized();
            // Ini Blacklist GUI
            blackList = new BlackListGUI();
        }

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
            {
                EnsureOverlay().SetEnabled(true);
            }
            else if (_ramOverlay != null)
            {
                _ramOverlay.SetEnabled(false);
                _ramOverlay.Dispose();
                _ramOverlay = null;
            }

            SettingsHandler.SaveOverlaySettings(enabled, SettingsHandler.Current.OverlayPosition);
        }

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
            _overlayToggleItem.CheckedChanged += (_, _) =>
            {
                SetOverlayEnabled(_overlayToggleItem.Checked);
            };

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
                _ramOverlay = null;
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

        private ToolStripMenuItem MakePosItem(string text, OverlayPosition position)
        {
            var item = new ToolStripMenuItem(text)
            {
                Checked = SettingsHandler.GetOverlayPosition() == position
            };

            item.Click += (_, _) =>
            {
                // 1) save first (works even if overlay isn't created yet)
                SettingsHandler.SaveOverlaySettings(
                    SettingsHandler.Current.OverlayEnabled,
                    position.ToString());

                // 2) apply only if overlay is currently alive
                _ramOverlay?.SetPosition(position);

                // 3) refresh checkmarks in the submenu
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

        private void RestoreFromTray()
        {
            Show();
            WindowState = FormWindowState.Normal;
            Activate();
            TopMost = true;
            TopMost = false;
        }

        // Check Start as Minimized added for folks that wants to save 1 second of their time XD
        private void CheckForStartAsMinimized()
        {
            if (!materialCheckbox4.Checked)
                return;

            _ = Task.Delay(50).ContinueWith(_ =>
            {
                if (!IsDisposed)
                    BeginInvoke(Hide);
            });
        }

        // Check Perms
        private void CheckCachedCleanPerm()
        {
            // Check required perms
            if (materialCheckbox3.Checked && !IsAdministrator())
                RestartAsAdmin(Environment.ProcessPath ?? Application.ExecutablePath);
        }

        // Completely forgot about it, teehee. Function is pretty self explanatory.
        private void CheckForExistingTimer()
        {
            bypass = true;
            materialComboBox1_SelectedIndexChanged(this, null);
        }

        // Manual Clean Button
        private async void button1_Click(object sender, EventArgs e)
        {
            await StartCleanAsync();
        }

        // Set Interval
        private void materialComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AppStarted)
            {
                if (!bypass)
                    // Save Setting
                    SettingsHandler.SaveSettings();
                else
                    // Reset save settings bypass
                    bypass = false;
                // Proceed
                if (materialComboBox1.SelectedItem.ToString() != "OFF")
                {
                    int milliseconds = Int32.Parse(materialComboBox1.SelectedItem.ToString()) * 60000;
                    CleanerTimer.Interval = milliseconds;
                    CleanerTimer.Start();
                }
                else
                    CleanerTimer.Stop();
            }
        }

        // Cleaner Interval
        private async void CleanerTimer_Tick(object sender, EventArgs e)
        {
            await StartCleanAsync();
        }

        // Start MemClean Routine (old/unused)
        //private void StartClean() =>
        //    MemoryCleaner.CleanMem(materialCheckbox2.Checked, materialCheckbox3.Checked);

        // Start MemClean Routine
        private Task StartCleanAsync() =>
            MemoryCleaner.CleanMemAsync(materialCheckbox2.Checked, materialCheckbox3.Checked);

        // Start with Windows
        private void materialCheckbox1_CheckedChanged(object sender, EventArgs e)
        {
            if (AppStarted)
            {
                // Save Setting
                SettingsHandler.SaveSettings();
                // Proceed
                if (materialCheckbox1.Checked)
                {
                    /* This bit was taken by stackoverflow */
                    WshShell wshShell = new WshShell();
                    IWshShortcut shortcut;
                    string startUpFolderPath =
                      Environment.GetFolderPath(Environment.SpecialFolder.Startup);

                    // Create the shortcut
                    shortcut =
                      (IWshShortcut)wshShell.CreateShortcut(
                        startUpFolderPath + "\\" +
                        Application.ProductName + ".lnk");

                    shortcut.TargetPath = Application.ExecutablePath;
                    shortcut.WorkingDirectory = Application.StartupPath;
                    shortcut.Description = "Memory Cleaner Startup";
                    //shortcut.IconLocation = Application.StartupPath + @"\App.ico";
                    shortcut.Save();
                    /* End of said bit */
                }
                else
                {
                    // Delete startup shortcut
                    System.IO.File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\" + Application.ProductName + ".lnk");
                }
            }
        }

        // Reset Log Text
        public void ResetLauncherLog()
        {
            if (IsDisposed) return;

            if (InvokeRequired)
            {
                BeginInvoke(ResetLauncherLog);
                return;
            }

            materialMultiLineTextBox21.Text = "Start of log";
        }

        // Add Logs to Form
        public void AddLauncherLog(string log)
        {
            if (IsDisposed) return;

            if (InvokeRequired)
            {
                BeginInvoke(() => AddLauncherLog(log));
                return;
            }

            materialMultiLineTextBox21.Text += Environment.NewLine + log;
        }

        // Show Processes in logs
        private void materialCheckbox2_CheckedChanged(object sender, EventArgs e)
        {
            if (AppStarted)
                // Save Setting
                SettingsHandler.SaveSettings();
        }

        // Minimize to tray button
        private void materialButton1_Click(object sender, EventArgs e) =>
            MinimizeToTray();

        // Minimize to tray
        private void MinimizeToTray() =>
            Hide();

        // Recover window from tray
        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                RestoreFromTray();
        }

        // About
        private void materialButton3_Click(object sender, EventArgs e) =>
            ReaLTaiizor.Controls.MaterialMessageBox.Show($"Made by Endless (Kogaruh){Environment.NewLine}Version: {appver}{Environment.NewLine}Run as admin for best results!");

        // Toggle Standby Cache Memory Clear (with admin check)
        private void materialCheckbox3_CheckedChanged(object sender, EventArgs e)
        {
            if (AppStarted)
                // Save Setting
                SettingsHandler.SaveSettings();

            // Check required perms
            CheckCachedCleanPerm();
        }

        // Check if ran as admin
        private static bool IsAdministrator()
        {
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        // Restart as admin
        private void RestartAsAdmin(string exe)
        {
            var proc = new Process
            {
                StartInfo =
                {
                    FileName = exe,
                    UseShellExecute = true,
                    Verb = "runas"
                }
            };
            try
            {
                if (proc.Start())
                    Process.GetCurrentProcess().Kill();
                else
                    materialCheckbox3.Checked = false;
            }
            catch { materialCheckbox3.Checked = false; }
        }

        // Save start as minimized
        private void materialCheckbox4_CheckedChanged(object sender, EventArgs e)
        {
            if (AppStarted)
                // Save Setting
                SettingsHandler.SaveSettings();
        }

        // Show Blacklist GUI
        private void materialButton4_Click(object sender, EventArgs e)
        {
            this.Hide();
            blackList.ShowDialog();
            this.Show();
            this.TopMost = true;
            Thread.Sleep(50);
            this.TopMost = false;
        }
    }
}

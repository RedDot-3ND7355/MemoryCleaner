using IWshRuntimeLibrary;
using MaterialSkin;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoryCleaner
{
    public partial class Form1 : MaterialSkin.Controls.MaterialForm
    {
        // Globals
        public readonly MaterialSkinManager materialSkinManager;
        public _MemoryCleaner MemoryCleaner = new _MemoryCleaner();
        public static Form1 CurrentForm;
        private bool AppStarted = false;
        private bool bypass = false;
        private string appver = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion.Replace(".0.0", "");

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
            // Set Minimized if config present
            CheckForStartAsMinimized();
        }

        // Check Start as Minimized added for folks that wants to save 1 second of their time XD
        private void CheckForStartAsMinimized()
        {
            if (materialCheckbox4.Checked)
            {
                notifyIcon1.Visible = true;
                Task.Delay(50).ContinueWith(delegate
                {
                    CheckForIllegalCrossThreadCalls = false;
                    this.Hide();
                });
            }
        }

        // Check Perms
        private void CheckCachedCleanPerm()
        {
            // Check required perms
            if (materialCheckbox3.Checked && !IsAdministrator())
                RestartAsAdmin(Assembly.GetExecutingAssembly().Location);
        }

        // Completely forgot about it, teehee. Function is pretty self explanatory.
        private void CheckForExistingTimer()
        {
            bypass = true;
            materialComboBox1_SelectedIndexChanged(this, null);
        }

        // Manual Clean Button
        private void button1_Click(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            // Start a thread that calls a parameterized instance method.
            Thread thread1 = new Thread(new ThreadStart(StartClean));
            thread1.Start();
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
        private void CleanerTimer_Tick(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            // Start a thread that calls a parameterized instance method.
            Thread thread1 = new Thread(new ThreadStart(StartClean));
            thread1.Start();
        }

        // Start MemClean Routine
        private void StartClean() =>
            MemoryCleaner.CleanMem(materialCheckbox2.Checked, materialCheckbox3.Checked);

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
            materialMultiLineTextBox21.Text = "Start of log";
        }

        // Add Logs to Form
        public void AddLauncherLog(string log)
        {
            // Proceed
            materialMultiLineTextBox21.Text += Environment.NewLine + log;

        }

        // Show Processes in logs
        private void materialCheckbox2_CheckedChanged(object sender, EventArgs e)
        {
            if (AppStarted)
                // Save Setting
                SettingsHandler.SaveSettings();
        }

        // Minimize to tray
        private void materialButton1_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = true;
            this.Hide();
        }

        // Recover window from tray
        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            this.Show();
            notifyIcon1.Visible = false;
        }

        // About
        private void materialButton3_Click(object sender, EventArgs e) =>
            MaterialSkin.Controls.MaterialMessageBox.Show($"Made by Endless (Kogaruh){Environment.NewLine}Version: {appver}{Environment.NewLine}Run as admin for best results!");

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
            } catch { materialCheckbox3.Checked = false; }
        }

        // Save start as minimized
        private void materialCheckbox4_CheckedChanged(object sender, EventArgs e)
        {
            if (AppStarted)
                // Save Setting
                SettingsHandler.SaveSettings();
        }
    }
}

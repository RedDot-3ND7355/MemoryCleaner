using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoryCleaner
{
    public static class SettingsHandler
    {
        static string default_path = Path.GetDirectoryName(Application.ExecutablePath) + "\\Settings.conf";
        static string default_text = $"Interval = OFF{Environment.NewLine}Startup = false{Environment.NewLine}Advanced = false{Environment.NewLine}Cached = true";

        // Initialize Settings
        public static void IniSettings() 
        {
            // Check & Create if needed
            if (!File.Exists(default_path))
                File.WriteAllText(default_path, default_text);
        }

        // Save Settings
        public static void SaveSettings() 
        {
            // Save Current Settings
            File.WriteAllText(default_path, $"Interval = {Form1.CurrentForm.materialComboBox1.SelectedItem.ToString()}{Environment.NewLine}Startup = {Form1.CurrentForm.materialCheckbox1.Checked.ToString()}{Environment.NewLine}Advanced = {Form1.CurrentForm.materialCheckbox2.Checked.ToString()}{Environment.NewLine}Cached = {Form1.CurrentForm.materialCheckbox3.Checked.ToString()}");
        }

        // Read Settings
        public static void ReadSettings() 
        {
            // Read Setting File
            string[] settings = File.ReadAllLines(default_path);
            foreach (string setting in settings)
            {
                string _setting = setting;
                if (setting.Contains("Interval = ")) // string search
                {
                    _setting = setting.Replace("Interval = ", "");
                    Form1.CurrentForm.materialComboBox1.SelectedItem = _setting;
                }
                if (setting.Contains("Startup = ")) // boolean
                {
                    _setting = setting.Replace("Startup = ", "");
                    Form1.CurrentForm.materialCheckbox1.Checked = bool.Parse(_setting);
                }
                if (setting.Contains("Advanced = ")) // boolean
                {
                    _setting = setting.Replace("Advanced = ", "");
                    Form1.CurrentForm.materialCheckbox2.Checked = bool.Parse(_setting);
                }
                if (settings.Contains("Cached = ")) // boolean
                {
                    _setting = setting.Replace("Cached = ", "");
                    Form1.CurrentForm.materialCheckbox3.Checked = bool.Parse(_setting);
                }
            }
        }
    }
}

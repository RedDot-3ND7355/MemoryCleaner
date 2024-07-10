using MaterialSkin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace MemoryCleaner
{
    public partial class BlackListGUI : MaterialSkin.Controls.MaterialForm
    {
        // Globals
        public readonly MaterialSkinManager materialSkinManager;

        public BlackListGUI()
        {
            InitializeComponent();
            // Set material colors
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.EnforceBackcolorOnAllComponents = true;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            // Ini Blacklist and Load it
            BlacklistHandler.IniBL();
            // Show loaded content into table
            LoadBL();
        }

        // Load BL into Table
        private void LoadBL()
        {
            materialListBox1.BeginUpdate();
            if (materialListBox1.Items.Count > 0)
                materialListBox1.Items.Clear();
            foreach (var processes in BlacklistHandler.blacklisted_processes)
                materialListBox1.AddItem(new MaterialListBoxItem(processes));
            materialListBox1.EndUpdate();
        }

        // Add Button (Selected Items from Prompt)
        private void materialButton1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "exe files (*.exe)|*.exe";
            if (DialogResult.OK == openFileDialog1.ShowDialog())
            {
                foreach (var process_name in openFileDialog1.FileNames)
                    BlacklistHandler.blacklisted_processes.Add(Path.GetFileName(process_name));
                // Save changes
                BlacklistHandler.SaveBL();
                // Reload
                LoadBL();
            }
        }

        // Remove Button (Selected Items)
        private void materialButton2_Click(object sender, EventArgs e)
        {
            // Seperate items to remove from control for many items
            foreach (MaterialListBoxItem item in materialListBox1.SelectedItems)
                BlacklistHandler.blacklisted_processes.Remove(item.ToString());
            // Seperate items to remove from control for single item
            if (materialListBox1.SelectedItem != null)
                BlacklistHandler.blacklisted_processes.Remove(materialListBox1.SelectedItem.Text);
            // Save changes
            BlacklistHandler.SaveBL();
            // Reload
            LoadBL();
            // Reset Selected Item
            materialListBox1.SelectedIndex = -1;
        }
    }
}

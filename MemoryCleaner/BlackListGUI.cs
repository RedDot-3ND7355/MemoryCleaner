using ReaLTaiizor.Child.Material;
using ReaLTaiizor.Manager;
using System;
using System.IO;
using System.Windows.Forms;

namespace MemoryCleaner
{
    public partial class BlackListGUI : ReaLTaiizor.Forms.MaterialForm
    {
        public readonly MaterialSkinManager materialSkinManager;

        public BlackListGUI()
        {
            InitializeComponent();
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.EnforceBackcolorOnAllComponents = true;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;

            BlacklistHandler.IniBL();
            LoadBL();
        }

        private void LoadBL()
        {
            materialListBox1.BeginUpdate();
            if (materialListBox1.Items.Count > 0)
                materialListBox1.Items.Clear();

            foreach (string process in BlacklistHandler.blacklisted_processes)
                materialListBox1.AddItem(new MaterialListBoxItem(process));

            materialListBox1.EndUpdate();
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "exe files (*.exe)|*.exe";
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return;

            foreach (string path in openFileDialog1.FileNames)
            {
                string name = Path.GetFileName(path);
                if (!string.IsNullOrWhiteSpace(name))
                    BlacklistHandler.blacklisted_processes.Add(name);
            }

            BlacklistHandler.SaveBL();
            LoadBL();
        }

        private void materialButton2_Click(object sender, EventArgs e)
        {
            foreach (MaterialListBoxItem item in materialListBox1.SelectedItems)
            {
                string name = item.Text;
                if (!string.IsNullOrWhiteSpace(name))
                    BlacklistHandler.blacklisted_processes.Remove(name);
            }

            if (materialListBox1.SelectedItem != null)
            {
                string name = materialListBox1.SelectedItem.Text;
                if (!string.IsNullOrWhiteSpace(name))
                    BlacklistHandler.blacklisted_processes.Remove(name);
            }

            BlacklistHandler.SaveBL();
            LoadBL();
            materialListBox1.SelectedIndex = -1;
        }
    }
}
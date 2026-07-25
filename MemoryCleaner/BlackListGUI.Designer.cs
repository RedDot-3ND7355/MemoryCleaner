using ReaLTaiizor;

namespace MemoryCleaner
{
    partial class BlackListGUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BlackListGUI));
            materialButton1 = new ReaLTaiizor.Controls.MaterialButton();
            materialButton2 = new ReaLTaiizor.Controls.MaterialButton();
            materialListBox1 = new ReaLTaiizor.Controls.MaterialListBox();
            openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            SuspendLayout();
            // 
            // materialButton1
            // 
            materialButton1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            materialButton1.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            materialButton1.Depth = 0;
            materialButton1.HighEmphasis = true;
            materialButton1.Icon = null;
            materialButton1.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            materialButton1.Location = new System.Drawing.Point(7, 70);
            materialButton1.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            materialButton1.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            materialButton1.Name = "materialButton1";
            materialButton1.NoAccentTextColor = System.Drawing.Color.Empty;
            materialButton1.Size = new System.Drawing.Size(119, 36);
            materialButton1.TabIndex = 0;
            materialButton1.Text = "Add Process";
            materialButton1.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            materialButton1.UseAccentColor = false;
            materialButton1.UseVisualStyleBackColor = true;
            materialButton1.Click += materialButton1_Click;
            // 
            // materialButton2
            // 
            materialButton2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            materialButton2.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            materialButton2.Depth = 0;
            materialButton2.HighEmphasis = true;
            materialButton2.Icon = null;
            materialButton2.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            materialButton2.Location = new System.Drawing.Point(134, 70);
            materialButton2.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            materialButton2.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            materialButton2.Name = "materialButton2";
            materialButton2.NoAccentTextColor = System.Drawing.Color.Empty;
            materialButton2.Size = new System.Drawing.Size(148, 36);
            materialButton2.TabIndex = 1;
            materialButton2.Text = "Remove Process";
            materialButton2.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            materialButton2.UseAccentColor = false;
            materialButton2.UseVisualStyleBackColor = true;
            materialButton2.Click += materialButton2_Click;
            // 
            // materialListBox1
            // 
            materialListBox1.BackColor = System.Drawing.Color.Transparent;
            materialListBox1.BorderColor = System.Drawing.Color.Black;
            materialListBox1.Depth = 0;
            materialListBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            materialListBox1.Location = new System.Drawing.Point(7, 115);
            materialListBox1.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            materialListBox1.MultiSelect = true;
            materialListBox1.Name = "materialListBox1";
            materialListBox1.SelectedIndex = -1;
            materialListBox1.SelectedItem = null;
            materialListBox1.ShowScrollBar = true;
            materialListBox1.Size = new System.Drawing.Size(275, 321);
            materialListBox1.TabIndex = 2;
            materialListBox1.UseAccentColor = true;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            openFileDialog1.Multiselect = true;
            openFileDialog1.RestoreDirectory = true;
            // 
            // BlackListGUI
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            ClientSize = new System.Drawing.Size(292, 450);
            Controls.Add(materialListBox1);
            Controls.Add(materialButton2);
            Controls.Add(materialButton1);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "BlackListGUI";
            Sizable = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Blacklist Processes";
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private ReaLTaiizor.Controls.MaterialButton materialButton1;
        private ReaLTaiizor.Controls.MaterialButton materialButton2;
        private ReaLTaiizor.Controls.MaterialListBox materialListBox1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}
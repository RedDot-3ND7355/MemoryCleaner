namespace MemoryCleaner
{
    partial class Form1
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            materialCard1 = new ReaLTaiizor.Controls.MaterialCard();
            materialLabel3 = new ReaLTaiizor.Controls.MaterialLabel();
            materialLabel1 = new ReaLTaiizor.Controls.MaterialLabel();
            materialComboBox1 = new ReaLTaiizor.Controls.MaterialComboBox();
            materialLabel2 = new ReaLTaiizor.Controls.MaterialLabel();
            materialDivider1 = new ReaLTaiizor.Controls.MaterialDivider();
            materialCheckbox1 = new ReaLTaiizor.Controls.MaterialCheckBox();
            materialMultiLineTextBox21 = new ReaLTaiizor.Controls.MaterialMultiLineTextBoxEdit();
            materialLabel4 = new ReaLTaiizor.Controls.MaterialLabel();
            CleanerTimer = new System.Windows.Forms.Timer(components);
            materialCheckbox2 = new ReaLTaiizor.Controls.MaterialCheckBox();
            materialDivider2 = new ReaLTaiizor.Controls.MaterialDivider();
            materialButton1 = new ReaLTaiizor.Controls.MaterialButton();
            materialDivider3 = new ReaLTaiizor.Controls.MaterialDivider();
            materialButton2 = new ReaLTaiizor.Controls.MaterialButton();
            notifyIcon1 = new System.Windows.Forms.NotifyIcon(components);
            materialButton3 = new ReaLTaiizor.Controls.MaterialButton();
            materialCheckbox3 = new ReaLTaiizor.Controls.MaterialCheckBox();
            materialCheckbox4 = new ReaLTaiizor.Controls.MaterialCheckBox();
            materialLabel5 = new ReaLTaiizor.Controls.MaterialLabel();
            materialButton4 = new ReaLTaiizor.Controls.MaterialButton();
            materialCard1.SuspendLayout();
            SuspendLayout();
            // 
            // materialCard1
            // 
            materialCard1.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            materialCard1.Controls.Add(materialLabel3);
            materialCard1.Controls.Add(materialLabel1);
            materialCard1.Controls.Add(materialComboBox1);
            materialCard1.Depth = 0;
            materialCard1.ForeColor = System.Drawing.Color.FromArgb(222, 0, 0, 0);
            materialCard1.Location = new System.Drawing.Point(248, 109);
            materialCard1.Margin = new System.Windows.Forms.Padding(14);
            materialCard1.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            materialCard1.Name = "materialCard1";
            materialCard1.Padding = new System.Windows.Forms.Padding(14);
            materialCard1.Size = new System.Drawing.Size(252, 103);
            materialCard1.TabIndex = 1;
            // 
            // materialLabel3
            // 
            materialLabel3.AutoSize = true;
            materialLabel3.Depth = 0;
            materialLabel3.Font = new System.Drawing.Font("Roboto", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            materialLabel3.FontType = ReaLTaiizor.Manager.MaterialSkinManager.FontType.H5;
            materialLabel3.Location = new System.Drawing.Point(150, 54);
            materialLabel3.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            materialLabel3.Name = "materialLabel3";
            materialLabel3.Size = new System.Drawing.Size(87, 29);
            materialLabel3.TabIndex = 2;
            materialLabel3.Text = "Minutes";
            // 
            // materialLabel1
            // 
            materialLabel1.AutoSize = true;
            materialLabel1.Depth = 0;
            materialLabel1.Font = new System.Drawing.Font("Roboto", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            materialLabel1.FontType = ReaLTaiizor.Manager.MaterialSkinManager.FontType.H5;
            materialLabel1.HighEmphasis = true;
            materialLabel1.Location = new System.Drawing.Point(34, 10);
            materialLabel1.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            materialLabel1.Name = "materialLabel1";
            materialLabel1.Size = new System.Drawing.Size(180, 29);
            materialLabel1.TabIndex = 1;
            materialLabel1.Text = "Interval Cleaning";
            // 
            // materialComboBox1
            // 
            materialComboBox1.AutoResize = false;
            materialComboBox1.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            materialComboBox1.Depth = 0;
            materialComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            materialComboBox1.DropDownHeight = 174;
            materialComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            materialComboBox1.DropDownWidth = 121;
            materialComboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            materialComboBox1.ForeColor = System.Drawing.Color.FromArgb(222, 0, 0, 0);
            materialComboBox1.FormattingEnabled = true;
            materialComboBox1.IntegralHeight = false;
            materialComboBox1.ItemHeight = 43;
            materialComboBox1.Items.AddRange(new object[] { "OFF", "5", "10", "15", "20", "25", "30", "45", "60" });
            materialComboBox1.Location = new System.Drawing.Point(17, 46);
            materialComboBox1.MaxDropDownItems = 4;
            materialComboBox1.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.OUT;
            materialComboBox1.Name = "materialComboBox1";
            materialComboBox1.Size = new System.Drawing.Size(127, 49);
            materialComboBox1.StartIndex = 0;
            materialComboBox1.TabIndex = 0;
            materialComboBox1.SelectedIndexChanged += materialComboBox1_SelectedIndexChanged;
            // 
            // materialLabel2
            // 
            materialLabel2.AutoSize = true;
            materialLabel2.Depth = 0;
            materialLabel2.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            materialLabel2.Location = new System.Drawing.Point(27, 80);
            materialLabel2.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            materialLabel2.Name = "materialLabel2";
            materialLabel2.Size = new System.Drawing.Size(112, 19);
            materialLabel2.TabIndex = 2;
            materialLabel2.Text = "Manual Cleaner";
            // 
            // materialDivider1
            // 
            materialDivider1.BackColor = System.Drawing.Color.FromArgb(30, 0, 0, 0);
            materialDivider1.Depth = 0;
            materialDivider1.Enabled = false;
            materialDivider1.Location = new System.Drawing.Point(15, 160);
            materialDivider1.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            materialDivider1.Name = "materialDivider1";
            materialDivider1.Size = new System.Drawing.Size(227, 10);
            materialDivider1.TabIndex = 3;
            materialDivider1.Text = "materialDivider1";
            // 
            // materialCheckbox1
            // 
            materialCheckbox1.AutoSize = true;
            materialCheckbox1.Depth = 0;
            materialCheckbox1.Location = new System.Drawing.Point(17, 168);
            materialCheckbox1.Margin = new System.Windows.Forms.Padding(0);
            materialCheckbox1.MouseLocation = new System.Drawing.Point(-1, -1);
            materialCheckbox1.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            materialCheckbox1.Name = "materialCheckbox1";
            materialCheckbox1.ReadOnly = false;
            materialCheckbox1.Ripple = true;
            materialCheckbox1.Size = new System.Drawing.Size(127, 37);
            materialCheckbox1.TabIndex = 4;
            materialCheckbox1.Text = "Start on boot";
            materialCheckbox1.UseAccentColor = false;
            materialCheckbox1.UseVisualStyleBackColor = true;
            materialCheckbox1.CheckedChanged += materialCheckbox1_CheckedChanged;
            // 
            // materialMultiLineTextBox21
            // 
            materialMultiLineTextBox21.AnimateReadOnly = true;
            materialMultiLineTextBox21.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            materialMultiLineTextBox21.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            materialMultiLineTextBox21.Cursor = System.Windows.Forms.Cursors.IBeam;
            materialMultiLineTextBox21.Depth = 0;
            materialMultiLineTextBox21.HideSelection = true;
            materialMultiLineTextBox21.Location = new System.Drawing.Point(15, 238);
            materialMultiLineTextBox21.MaxLength = 9999999;
            materialMultiLineTextBox21.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.OUT;
            materialMultiLineTextBox21.Name = "materialMultiLineTextBox21";
            materialMultiLineTextBox21.PasswordChar = '\0';
            materialMultiLineTextBox21.ReadOnly = true;
            materialMultiLineTextBox21.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            materialMultiLineTextBox21.SelectedText = "";
            materialMultiLineTextBox21.SelectionLength = 0;
            materialMultiLineTextBox21.SelectionStart = 0;
            materialMultiLineTextBox21.ShortcutsEnabled = true;
            materialMultiLineTextBox21.Size = new System.Drawing.Size(485, 134);
            materialMultiLineTextBox21.TabIndex = 6;
            materialMultiLineTextBox21.TabStop = false;
            materialMultiLineTextBox21.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            materialMultiLineTextBox21.UseSystemPasswordChar = false;
            // 
            // materialLabel4
            // 
            materialLabel4.AutoSize = true;
            materialLabel4.Depth = 0;
            materialLabel4.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            materialLabel4.Location = new System.Drawing.Point(21, 216);
            materialLabel4.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            materialLabel4.Name = "materialLabel4";
            materialLabel4.Size = new System.Drawing.Size(93, 19);
            materialLabel4.TabIndex = 7;
            materialLabel4.Text = "Event Logger";
            // 
            // CleanerTimer
            // 
            CleanerTimer.Tick += CleanerTimer_Tick;
            // 
            // materialCheckbox2
            // 
            materialCheckbox2.AutoSize = true;
            materialCheckbox2.Depth = 0;
            materialCheckbox2.Location = new System.Drawing.Point(122, 208);
            materialCheckbox2.Margin = new System.Windows.Forms.Padding(0);
            materialCheckbox2.MouseLocation = new System.Drawing.Point(-1, -1);
            materialCheckbox2.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            materialCheckbox2.Name = "materialCheckbox2";
            materialCheckbox2.ReadOnly = false;
            materialCheckbox2.Ripple = true;
            materialCheckbox2.Size = new System.Drawing.Size(105, 37);
            materialCheckbox2.TabIndex = 8;
            materialCheckbox2.Text = "Advanced";
            materialCheckbox2.UseAccentColor = false;
            materialCheckbox2.UseVisualStyleBackColor = true;
            materialCheckbox2.CheckedChanged += materialCheckbox2_CheckedChanged;
            // 
            // materialDivider2
            // 
            materialDivider2.BackColor = System.Drawing.Color.FromArgb(30, 0, 0, 0);
            materialDivider2.Depth = 0;
            materialDivider2.Enabled = false;
            materialDivider2.Location = new System.Drawing.Point(157, 80);
            materialDivider2.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            materialDivider2.Name = "materialDivider2";
            materialDivider2.Size = new System.Drawing.Size(10, 132);
            materialDivider2.TabIndex = 9;
            materialDivider2.Text = "materialDivider2";
            // 
            // materialButton1
            // 
            materialButton1.AutoSize = false;
            materialButton1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            materialButton1.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            materialButton1.Depth = 0;
            materialButton1.HighEmphasis = true;
            materialButton1.Icon = null;
            materialButton1.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            materialButton1.Location = new System.Drawing.Point(172, 80);
            materialButton1.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            materialButton1.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            materialButton1.Name = "materialButton1";
            materialButton1.NoAccentTextColor = System.Drawing.Color.Empty;
            materialButton1.Size = new System.Drawing.Size(70, 75);
            materialButton1.TabIndex = 10;
            materialButton1.Text = "Minimize\r\nto \r\nTray";
            materialButton1.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            materialButton1.UseAccentColor = false;
            materialButton1.UseVisualStyleBackColor = true;
            materialButton1.Click += materialButton1_Click;
            // 
            // materialDivider3
            // 
            materialDivider3.BackColor = System.Drawing.Color.FromArgb(30, 0, 0, 0);
            materialDivider3.Depth = 0;
            materialDivider3.Enabled = false;
            materialDivider3.Location = new System.Drawing.Point(15, 202);
            materialDivider3.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            materialDivider3.Name = "materialDivider3";
            materialDivider3.Size = new System.Drawing.Size(227, 10);
            materialDivider3.TabIndex = 11;
            materialDivider3.Text = "materialDivider3";
            // 
            // materialButton2
            // 
            materialButton2.AutoSize = false;
            materialButton2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            materialButton2.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            materialButton2.Depth = 0;
            materialButton2.HighEmphasis = true;
            materialButton2.Icon = null;
            materialButton2.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            materialButton2.Location = new System.Drawing.Point(15, 109);
            materialButton2.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            materialButton2.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            materialButton2.Name = "materialButton2";
            materialButton2.NoAccentTextColor = System.Drawing.Color.Empty;
            materialButton2.Size = new System.Drawing.Size(137, 46);
            materialButton2.TabIndex = 12;
            materialButton2.Text = "Clean Memory";
            materialButton2.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            materialButton2.UseAccentColor = false;
            materialButton2.UseVisualStyleBackColor = true;
            materialButton2.Click += button1_Click;
            // 
            // notifyIcon1
            // 
            notifyIcon1.Icon = (System.Drawing.Icon)resources.GetObject("notifyIcon1.Icon");
            notifyIcon1.Text = "Memory Cleaner";
            notifyIcon1.MouseClick += notifyIcon1_MouseClick;
            // 
            // materialButton3
            // 
            materialButton3.AutoSize = false;
            materialButton3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            materialButton3.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            materialButton3.Depth = 0;
            materialButton3.HighEmphasis = true;
            materialButton3.Icon = null;
            materialButton3.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            materialButton3.Location = new System.Drawing.Point(172, 174);
            materialButton3.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            materialButton3.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            materialButton3.Name = "materialButton3";
            materialButton3.NoAccentTextColor = System.Drawing.Color.Empty;
            materialButton3.Size = new System.Drawing.Size(70, 23);
            materialButton3.TabIndex = 13;
            materialButton3.Text = "About";
            materialButton3.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            materialButton3.UseAccentColor = false;
            materialButton3.UseVisualStyleBackColor = true;
            materialButton3.Click += materialButton3_Click;
            // 
            // materialCheckbox3
            // 
            materialCheckbox3.AutoSize = true;
            materialCheckbox3.Checked = true;
            materialCheckbox3.CheckState = System.Windows.Forms.CheckState.Checked;
            materialCheckbox3.Depth = 0;
            materialCheckbox3.Location = new System.Drawing.Point(240, 208);
            materialCheckbox3.Margin = new System.Windows.Forms.Padding(0);
            materialCheckbox3.MouseLocation = new System.Drawing.Point(-1, -1);
            materialCheckbox3.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            materialCheckbox3.Name = "materialCheckbox3";
            materialCheckbox3.ReadOnly = false;
            materialCheckbox3.Ripple = true;
            materialCheckbox3.Size = new System.Drawing.Size(260, 37);
            materialCheckbox3.TabIndex = 14;
            materialCheckbox3.Text = "Clear Standby(Cached) Memory";
            materialCheckbox3.UseAccentColor = false;
            materialCheckbox3.UseVisualStyleBackColor = true;
            materialCheckbox3.CheckedChanged += materialCheckbox3_CheckedChanged;
            // 
            // materialCheckbox4
            // 
            materialCheckbox4.Depth = 0;
            materialCheckbox4.Location = new System.Drawing.Point(240, 75);
            materialCheckbox4.Margin = new System.Windows.Forms.Padding(0);
            materialCheckbox4.MouseLocation = new System.Drawing.Point(-1, -1);
            materialCheckbox4.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            materialCheckbox4.Name = "materialCheckbox4";
            materialCheckbox4.ReadOnly = false;
            materialCheckbox4.Ripple = true;
            materialCheckbox4.Size = new System.Drawing.Size(168, 37);
            materialCheckbox4.TabIndex = 15;
            materialCheckbox4.Text = "Start Minimized";
            materialCheckbox4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            materialCheckbox4.UseAccentColor = false;
            materialCheckbox4.UseVisualStyleBackColor = true;
            materialCheckbox4.CheckedChanged += materialCheckbox4_CheckedChanged;
            // 
            // materialLabel5
            // 
            materialLabel5.AutoSize = true;
            materialLabel5.Depth = 0;
            materialLabel5.Font = new System.Drawing.Font("Roboto", 34F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            materialLabel5.FontType = ReaLTaiizor.Manager.MaterialSkinManager.FontType.H4;
            materialLabel5.Location = new System.Drawing.Point(436, 71);
            materialLabel5.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            materialLabel5.Name = "materialLabel5";
            materialLabel5.Size = new System.Drawing.Size(64, 41);
            materialLabel5.TabIndex = 16;
            materialLabel5.Text = "v0.0";
            // 
            // materialButton4
            // 
            materialButton4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            materialButton4.BackColor = System.Drawing.SystemColors.ActiveCaption;
            materialButton4.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            materialButton4.Depth = 0;
            materialButton4.HighEmphasis = true;
            materialButton4.Icon = null;
            materialButton4.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            materialButton4.Location = new System.Drawing.Point(342, 25);
            materialButton4.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            materialButton4.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            materialButton4.Name = "materialButton4";
            materialButton4.NoAccentTextColor = System.Drawing.Color.Empty;
            materialButton4.Size = new System.Drawing.Size(165, 36);
            materialButton4.TabIndex = 17;
            materialButton4.Text = "Blacklist Process";
            materialButton4.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Text;
            materialButton4.UseAccentColor = false;
            materialButton4.UseVisualStyleBackColor = false;
            materialButton4.Click += materialButton4_Click;
            // 
            // Form1
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            ClientSize = new System.Drawing.Size(522, 391);
            Controls.Add(materialButton4);
            Controls.Add(materialButton3);
            Controls.Add(materialButton2);
            Controls.Add(materialDivider3);
            Controls.Add(materialButton1);
            Controls.Add(materialDivider2);
            Controls.Add(materialLabel4);
            Controls.Add(materialMultiLineTextBox21);
            Controls.Add(materialDivider1);
            Controls.Add(materialLabel2);
            Controls.Add(materialCard1);
            Controls.Add(materialCheckbox2);
            Controls.Add(materialCheckbox1);
            Controls.Add(materialCheckbox3);
            Controls.Add(materialCheckbox4);
            Controls.Add(materialLabel5);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Form1";
            Sizable = false;
            SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Very Simple Memory Cleaner";
            materialCard1.ResumeLayout(false);
            materialCard1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion
        private ReaLTaiizor.Controls.MaterialCard materialCard1;
        private ReaLTaiizor.Controls.MaterialLabel materialLabel1;
        private ReaLTaiizor.Controls.MaterialLabel materialLabel2;
        private ReaLTaiizor.Controls.MaterialLabel materialLabel3;
        private ReaLTaiizor.Controls.MaterialDivider materialDivider1;
        private ReaLTaiizor.Controls.MaterialMultiLineTextBoxEdit materialMultiLineTextBox21;
        private ReaLTaiizor.Controls.MaterialLabel materialLabel4;
        private System.Windows.Forms.Timer CleanerTimer;
        private ReaLTaiizor.Controls.MaterialDivider materialDivider2;
        private ReaLTaiizor.Controls.MaterialButton materialButton1;
        private ReaLTaiizor.Controls.MaterialDivider materialDivider3;
        private ReaLTaiizor.Controls.MaterialButton materialButton2;
        public ReaLTaiizor.Controls.MaterialComboBox materialComboBox1;
        public ReaLTaiizor.Controls.MaterialCheckBox materialCheckbox1;
        public ReaLTaiizor.Controls.MaterialCheckBox materialCheckbox2;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private ReaLTaiizor.Controls.MaterialButton materialButton3;
        public ReaLTaiizor.Controls.MaterialCheckBox materialCheckbox3;
        public ReaLTaiizor.Controls.MaterialCheckBox materialCheckbox4;
        private ReaLTaiizor.Controls.MaterialLabel materialLabel5;
        private ReaLTaiizor.Controls.MaterialButton materialButton4;
    }
}


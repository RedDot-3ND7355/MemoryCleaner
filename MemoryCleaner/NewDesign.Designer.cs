using System.Windows.Forms;

namespace MemoryCleaner
{
    partial class NewDesign
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel pnlStatus;
        private System.Windows.Forms.Label lblRAMFree;
        private System.Windows.Forms.Label lblMinutes;
        private System.Windows.Forms.ComboBox cmbIntervalUnit;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewDesign));
            circularramProgress1 = new CircularRAMProgress();
            cleanButton = new ReaLTaiizor.Controls.MaterialButton();
            materialCard1 = new ReaLTaiizor.Controls.MaterialCard();
            lblFreeRAM = new ReaLTaiizor.Controls.MaterialLabel();
            materialDivider1 = new ReaLTaiizor.Controls.MaterialDivider();
            lblUsedRAM = new ReaLTaiizor.Controls.MaterialLabel();
            advancedlogsCheckBox = new ReaLTaiizor.Controls.MaterialCheckBox();
            startonbootCheckBox = new ReaLTaiizor.Controls.MaterialCheckBox();
            clearstandbymemCheckBox = new ReaLTaiizor.Controls.MaterialCheckBox();
            startminimizedCheckBox = new ReaLTaiizor.Controls.MaterialCheckBox();
            intervalComboBox = new ReaLTaiizor.Controls.MaterialComboBox();
            materialLabel1 = new ReaLTaiizor.Controls.MaterialLabel();
            logTextBox = new ReaLTaiizor.Controls.MaterialMultiLineTextBoxEdit();
            blacklistButton = new ReaLTaiizor.Controls.MaterialButton();
            minimizeToTrayButton = new ReaLTaiizor.Controls.MaterialButton();
            aboutButton = new ReaLTaiizor.Controls.MaterialButton();
            materialLabel4 = new ReaLTaiizor.Controls.MaterialLabel();
            notifyIcon1 = new NotifyIcon(components);
            CleanerTimer = new Timer(components);
            ramUpdateTimer = new Timer(components);
            materialCard1.SuspendLayout();
            SuspendLayout();
            // 
            // circularramProgress1
            // 
            circularramProgress1.BackColor = System.Drawing.Color.Transparent;
            circularramProgress1.CenterText = "0%";
            circularramProgress1.ForeColor = System.Drawing.Color.White;
            circularramProgress1.Location = new System.Drawing.Point(4, 6);
            circularramProgress1.Maximum = 100;
            circularramProgress1.Name = "circularramProgress1";
            circularramProgress1.Size = new System.Drawing.Size(130, 130);
            circularramProgress1.TabIndex = 0;
            circularramProgress1.Value = 32;
            // 
            // cleanButton
            // 
            cleanButton.AutoSize = false;
            cleanButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            cleanButton.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            cleanButton.Depth = 0;
            cleanButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            cleanButton.HighEmphasis = true;
            cleanButton.Icon = null;
            cleanButton.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            cleanButton.Location = new System.Drawing.Point(7, 214);
            cleanButton.Margin = new Padding(4, 6, 4, 6);
            cleanButton.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            cleanButton.Name = "cleanButton";
            cleanButton.NoAccentTextColor = System.Drawing.Color.Empty;
            cleanButton.Size = new System.Drawing.Size(347, 36);
            cleanButton.TabIndex = 13;
            cleanButton.Text = "Clean Memory";
            cleanButton.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            cleanButton.UseAccentColor = false;
            cleanButton.UseVisualStyleBackColor = true;
            cleanButton.Click += cleanButton_Click;
            // 
            // materialCard1
            // 
            materialCard1.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            materialCard1.Controls.Add(lblFreeRAM);
            materialCard1.Controls.Add(materialDivider1);
            materialCard1.Controls.Add(lblUsedRAM);
            materialCard1.Controls.Add(circularramProgress1);
            materialCard1.Depth = 0;
            materialCard1.ForeColor = System.Drawing.Color.FromArgb(222, 0, 0, 0);
            materialCard1.Location = new System.Drawing.Point(7, 69);
            materialCard1.Margin = new Padding(14);
            materialCard1.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            materialCard1.Name = "materialCard1";
            materialCard1.Padding = new Padding(14);
            materialCard1.Size = new System.Drawing.Size(347, 142);
            materialCard1.TabIndex = 14;
            // 
            // lblFreeRAM
            // 
            lblFreeRAM.Depth = 0;
            lblFreeRAM.Font = new System.Drawing.Font("Roboto Medium", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            lblFreeRAM.FontType = ReaLTaiizor.Manager.MaterialSkinManager.FontType.H6;
            lblFreeRAM.Location = new System.Drawing.Point(136, 81);
            lblFreeRAM.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            lblFreeRAM.Name = "lblFreeRAM";
            lblFreeRAM.Size = new System.Drawing.Size(200, 40);
            lblFreeRAM.TabIndex = 3;
            lblFreeRAM.Text = "Free: 10.7 GB";
            lblFreeRAM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // materialDivider1
            // 
            materialDivider1.BackColor = System.Drawing.Color.Red;
            materialDivider1.Depth = 0;
            materialDivider1.Location = new System.Drawing.Point(140, 63);
            materialDivider1.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            materialDivider1.Name = "materialDivider1";
            materialDivider1.Size = new System.Drawing.Size(196, 15);
            materialDivider1.TabIndex = 2;
            materialDivider1.Text = "materialDivider1";
            // 
            // lblUsedRAM
            // 
            lblUsedRAM.Depth = 0;
            lblUsedRAM.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            lblUsedRAM.FontType = ReaLTaiizor.Manager.MaterialSkinManager.FontType.Subtitle1;
            lblUsedRAM.Location = new System.Drawing.Point(136, 17);
            lblUsedRAM.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            lblUsedRAM.Name = "lblUsedRAM";
            lblUsedRAM.Size = new System.Drawing.Size(200, 40);
            lblUsedRAM.TabIndex = 1;
            lblUsedRAM.Text = "Used: 32% (5.1/15.8 GB)";
            lblUsedRAM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // advancedlogsCheckBox
            // 
            advancedlogsCheckBox.AutoSize = true;
            advancedlogsCheckBox.Depth = 0;
            advancedlogsCheckBox.Location = new System.Drawing.Point(7, 291);
            advancedlogsCheckBox.Margin = new Padding(0);
            advancedlogsCheckBox.MouseLocation = new System.Drawing.Point(-1, -1);
            advancedlogsCheckBox.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            advancedlogsCheckBox.Name = "advancedlogsCheckBox";
            advancedlogsCheckBox.ReadOnly = false;
            advancedlogsCheckBox.Ripple = true;
            advancedlogsCheckBox.Size = new System.Drawing.Size(144, 37);
            advancedlogsCheckBox.TabIndex = 17;
            advancedlogsCheckBox.Text = "Advanced Logs";
            advancedlogsCheckBox.UseAccentColor = false;
            advancedlogsCheckBox.UseVisualStyleBackColor = true;
            advancedlogsCheckBox.CheckedChanged += advancedlogsCheckBox_CheckedChanged;
            // 
            // startonbootCheckBox
            // 
            startonbootCheckBox.AutoSize = true;
            startonbootCheckBox.Depth = 0;
            startonbootCheckBox.Location = new System.Drawing.Point(7, 254);
            startonbootCheckBox.Margin = new Padding(0);
            startonbootCheckBox.MouseLocation = new System.Drawing.Point(-1, -1);
            startonbootCheckBox.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            startonbootCheckBox.Name = "startonbootCheckBox";
            startonbootCheckBox.ReadOnly = false;
            startonbootCheckBox.Ripple = true;
            startonbootCheckBox.Size = new System.Drawing.Size(127, 37);
            startonbootCheckBox.TabIndex = 16;
            startonbootCheckBox.Text = "Start on boot";
            startonbootCheckBox.UseAccentColor = false;
            startonbootCheckBox.UseVisualStyleBackColor = true;
            startonbootCheckBox.CheckedChanged += startonbootCheckBox_CheckedChanged;
            // 
            // clearstandbymemCheckBox
            // 
            clearstandbymemCheckBox.AutoSize = true;
            clearstandbymemCheckBox.Checked = true;
            clearstandbymemCheckBox.CheckState = CheckState.Checked;
            clearstandbymemCheckBox.Depth = 0;
            clearstandbymemCheckBox.Location = new System.Drawing.Point(159, 291);
            clearstandbymemCheckBox.Margin = new Padding(0);
            clearstandbymemCheckBox.MouseLocation = new System.Drawing.Point(-1, -1);
            clearstandbymemCheckBox.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            clearstandbymemCheckBox.Name = "clearstandbymemCheckBox";
            clearstandbymemCheckBox.ReadOnly = false;
            clearstandbymemCheckBox.Ripple = true;
            clearstandbymemCheckBox.Size = new System.Drawing.Size(196, 37);
            clearstandbymemCheckBox.TabIndex = 18;
            clearstandbymemCheckBox.Text = "Clear Standby Memory";
            clearstandbymemCheckBox.UseAccentColor = false;
            clearstandbymemCheckBox.UseVisualStyleBackColor = true;
            clearstandbymemCheckBox.CheckedChanged += clearstandbymemCheckBox_CheckedChanged;
            // 
            // startminimizedCheckBox
            // 
            startminimizedCheckBox.Depth = 0;
            startminimizedCheckBox.Location = new System.Drawing.Point(158, 254);
            startminimizedCheckBox.Margin = new Padding(0);
            startminimizedCheckBox.MouseLocation = new System.Drawing.Point(-1, -1);
            startminimizedCheckBox.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            startminimizedCheckBox.Name = "startminimizedCheckBox";
            startminimizedCheckBox.ReadOnly = false;
            startminimizedCheckBox.Ripple = true;
            startminimizedCheckBox.Size = new System.Drawing.Size(196, 37);
            startminimizedCheckBox.TabIndex = 19;
            startminimizedCheckBox.Text = "Start Minimized";
            startminimizedCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            startminimizedCheckBox.UseAccentColor = false;
            startminimizedCheckBox.UseVisualStyleBackColor = true;
            startminimizedCheckBox.CheckedChanged += startminimizedCheckBox_CheckedChanged;
            // 
            // intervalComboBox
            // 
            intervalComboBox.AutoResize = false;
            intervalComboBox.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            intervalComboBox.Depth = 0;
            intervalComboBox.DrawMode = DrawMode.OwnerDrawVariable;
            intervalComboBox.DropDownHeight = 118;
            intervalComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            intervalComboBox.DropDownWidth = 121;
            intervalComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            intervalComboBox.ForeColor = System.Drawing.Color.FromArgb(222, 0, 0, 0);
            intervalComboBox.FormattingEnabled = true;
            intervalComboBox.IntegralHeight = false;
            intervalComboBox.ItemHeight = 29;
            intervalComboBox.Items.AddRange(new object[] { "OFF", "5", "10", "15", "20", "25", "30", "45", "60" });
            intervalComboBox.Location = new System.Drawing.Point(197, 331);
            intervalComboBox.MaxDropDownItems = 4;
            intervalComboBox.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.OUT;
            intervalComboBox.Name = "intervalComboBox";
            intervalComboBox.Size = new System.Drawing.Size(91, 35);
            intervalComboBox.StartIndex = 0;
            intervalComboBox.TabIndex = 20;
            intervalComboBox.UseTallSize = false;
            intervalComboBox.SelectedIndexChanged += intervalComboBox_SelectedIndexChanged;
            // 
            // materialLabel1
            // 
            materialLabel1.Depth = 0;
            materialLabel1.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            materialLabel1.FontType = ReaLTaiizor.Manager.MaterialSkinManager.FontType.Subtitle1;
            materialLabel1.HighEmphasis = true;
            materialLabel1.Location = new System.Drawing.Point(116, 329);
            materialLabel1.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            materialLabel1.Name = "materialLabel1";
            materialLabel1.Size = new System.Drawing.Size(75, 38);
            materialLabel1.TabIndex = 21;
            materialLabel1.Text = "Interval\r\nCleaning";
            materialLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // logTextBox
            // 
            logTextBox.AnimateReadOnly = true;
            logTextBox.BackgroundImageLayout = ImageLayout.None;
            logTextBox.CharacterCasing = CharacterCasing.Normal;
            logTextBox.Cursor = Cursors.IBeam;
            logTextBox.Depth = 0;
            logTextBox.Dock = DockStyle.Bottom;
            logTextBox.HideSelection = true;
            logTextBox.Location = new System.Drawing.Point(3, 372);
            logTextBox.MaxLength = 9999999;
            logTextBox.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.OUT;
            logTextBox.Name = "logTextBox";
            logTextBox.PasswordChar = '\0';
            logTextBox.ReadOnly = true;
            logTextBox.ScrollBars = ScrollBars.Vertical;
            logTextBox.SelectedText = "";
            logTextBox.SelectionLength = 0;
            logTextBox.SelectionStart = 0;
            logTextBox.ShortcutsEnabled = true;
            logTextBox.Size = new System.Drawing.Size(356, 241);
            logTextBox.TabIndex = 22;
            logTextBox.TabStop = false;
            logTextBox.TextAlign = HorizontalAlignment.Left;
            logTextBox.UseSystemPasswordChar = false;
            logTextBox.Enter += logTextBox_Enter;
            // 
            // blacklistButton
            // 
            blacklistButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            blacklistButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            blacklistButton.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            blacklistButton.Depth = 0;
            blacklistButton.HighEmphasis = true;
            blacklistButton.Icon = null;
            blacklistButton.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            blacklistButton.Location = new System.Drawing.Point(189, 30);
            blacklistButton.Margin = new Padding(4, 6, 4, 6);
            blacklistButton.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            blacklistButton.Name = "blacklistButton";
            blacklistButton.NoAccentTextColor = System.Drawing.Color.Empty;
            blacklistButton.Size = new System.Drawing.Size(165, 36);
            blacklistButton.TabIndex = 24;
            blacklistButton.Text = "Blacklist Process";
            blacklistButton.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Outlined;
            blacklistButton.UseAccentColor = false;
            blacklistButton.UseVisualStyleBackColor = false;
            blacklistButton.Click += blacklistButton_Click;
            // 
            // minimizeToTrayButton
            // 
            minimizeToTrayButton.AutoSize = false;
            minimizeToTrayButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            minimizeToTrayButton.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            minimizeToTrayButton.Depth = 0;
            minimizeToTrayButton.HighEmphasis = true;
            minimizeToTrayButton.Icon = null;
            minimizeToTrayButton.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            minimizeToTrayButton.Location = new System.Drawing.Point(7, 30);
            minimizeToTrayButton.Margin = new Padding(4, 6, 4, 6);
            minimizeToTrayButton.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            minimizeToTrayButton.Name = "minimizeToTrayButton";
            minimizeToTrayButton.NoAccentTextColor = System.Drawing.Color.Empty;
            minimizeToTrayButton.Size = new System.Drawing.Size(174, 36);
            minimizeToTrayButton.TabIndex = 25;
            minimizeToTrayButton.Text = "Minimize to Tray";
            minimizeToTrayButton.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            minimizeToTrayButton.UseAccentColor = false;
            minimizeToTrayButton.UseVisualStyleBackColor = true;
            minimizeToTrayButton.Click += minimizeToTrayButton_Click;
            // 
            // aboutButton
            // 
            aboutButton.AutoSize = false;
            aboutButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            aboutButton.Density = ReaLTaiizor.Controls.MaterialButton.MaterialButtonDensity.Default;
            aboutButton.Depth = 0;
            aboutButton.HighEmphasis = true;
            aboutButton.Icon = null;
            aboutButton.IconType = ReaLTaiizor.Controls.MaterialButton.MaterialIconType.Rebase;
            aboutButton.Location = new System.Drawing.Point(7, 328);
            aboutButton.Margin = new Padding(4, 6, 4, 6);
            aboutButton.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            aboutButton.Name = "aboutButton";
            aboutButton.NoAccentTextColor = System.Drawing.Color.Empty;
            aboutButton.Size = new System.Drawing.Size(102, 38);
            aboutButton.TabIndex = 26;
            aboutButton.Text = "About";
            aboutButton.Type = ReaLTaiizor.Controls.MaterialButton.MaterialButtonType.Contained;
            aboutButton.UseAccentColor = false;
            aboutButton.UseVisualStyleBackColor = true;
            aboutButton.Click += aboutButton_Click;
            // 
            // materialLabel4
            // 
            materialLabel4.AutoSize = true;
            materialLabel4.Depth = 0;
            materialLabel4.Font = new System.Drawing.Font("Roboto", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            materialLabel4.FontType = ReaLTaiizor.Manager.MaterialSkinManager.FontType.H5;
            materialLabel4.Location = new System.Drawing.Point(294, 333);
            materialLabel4.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.HOVER;
            materialLabel4.Name = "materialLabel4";
            materialLabel4.Size = new System.Drawing.Size(47, 29);
            materialLabel4.TabIndex = 27;
            materialLabel4.Text = "Min.";
            // 
            // notifyIcon1
            // 
            notifyIcon1.Icon = (System.Drawing.Icon)resources.GetObject("notifyIcon1.Icon");
            notifyIcon1.Text = "Memory Cleaner";
            notifyIcon1.MouseClick += notifyIcon1_MouseClick;
            // 
            // CleanerTimer
            // 
            CleanerTimer.Tick += CleanerTimer_Tick;
            // 
            // ramUpdateTimer
            // 
            ramUpdateTimer.Interval = 2000;
            // 
            // NewDesign
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new System.Drawing.Size(362, 616);
            Controls.Add(materialLabel4);
            Controls.Add(aboutButton);
            Controls.Add(minimizeToTrayButton);
            Controls.Add(blacklistButton);
            Controls.Add(logTextBox);
            Controls.Add(materialLabel1);
            Controls.Add(intervalComboBox);
            Controls.Add(advancedlogsCheckBox);
            Controls.Add(startonbootCheckBox);
            Controls.Add(clearstandbymemCheckBox);
            Controls.Add(startminimizedCheckBox);
            Controls.Add(materialCard1);
            Controls.Add(cleanButton);
            FormStyle = ReaLTaiizor.Enum.Material.FormStyles.ActionBar_None;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "NewDesign";
            Padding = new Padding(3, 24, 3, 3);
            Sizable = false;
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Very Simple Memory Cleaner";
            materialCard1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        private ReaLTaiizor.Controls.MaterialButton blacklistButton;
        private ReaLTaiizor.Controls.MaterialButton cleanButton;
        private CircularRAMProgress circularramProgress1;
        private ReaLTaiizor.Controls.MaterialCard materialCard1;
        public ReaLTaiizor.Controls.MaterialCheckBox advancedlogsCheckBox;
        public ReaLTaiizor.Controls.MaterialCheckBox startonbootCheckBox;
        public ReaLTaiizor.Controls.MaterialCheckBox clearstandbymemCheckBox;
        public ReaLTaiizor.Controls.MaterialCheckBox startminimizedCheckBox;
        public ReaLTaiizor.Controls.MaterialComboBox intervalComboBox;
        private ReaLTaiizor.Controls.MaterialLabel materialLabel1;
        private ReaLTaiizor.Controls.MaterialMultiLineTextBoxEdit logTextBox;
        private ReaLTaiizor.Controls.MaterialButton minimizeToTrayButton;
        private ReaLTaiizor.Controls.MaterialDivider materialDivider1;
        private ReaLTaiizor.Controls.MaterialLabel lblUsedRAM;
        private ReaLTaiizor.Controls.MaterialLabel lblFreeRAM;
        private ReaLTaiizor.Controls.MaterialButton aboutButton;
        private ReaLTaiizor.Controls.MaterialLabel materialLabel4;
        private NotifyIcon notifyIcon1;
        private Timer CleanerTimer;
        private Timer ramUpdateTimer;
    }
}
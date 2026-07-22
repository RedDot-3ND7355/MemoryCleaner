using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MemoryCleaner
{
    public partial class CircularRAMProgress : UserControl
    {
        private int _value = 0;
        private int _maximum = 100;
        private int _animatedValue = 0; // For smooth animation
        private string _centerText = "0%";

        private Timer animationTimer;

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int Value
        {
            get => _value;
            set
            {
                _value = Math.Max(0, Math.Min(_maximum, value));
                animationTimer.Start(); // Start smooth transition
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int Maximum
        {
            get => _maximum;
            set { _maximum = value; Invalidate(); }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string CenterText
        {
            get => _centerText;
            set { _centerText = value ?? ""; Invalidate(); }
        }

        public CircularRAMProgress()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.UserPaint | ControlStyles.ResizeRedraw, true);

            Size = new Size(150, 150);
            BackColor = Color.Transparent;
            ForeColor = Color.White;

            animationTimer = new Timer { Interval = 30 }; // ~60 FPS
            animationTimer.Tick += AnimationTimer_Tick;
        }

        private void AnimationTimer_Tick(object? sender, EventArgs e)
        {
            if (Math.Abs(_animatedValue - _value) <= 1)
            {
                _animatedValue = _value;
                animationTimer.Stop();
            }
            else
            {
                _animatedValue += (_value > _animatedValue) ? 1 : -1;
            }

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.Clear(BackColor);

            int padding = 12;
            Rectangle rect = new Rectangle(padding, padding, Width - padding * 2, Height - padding * 2);

            float progressAngle = (float)_animatedValue / _maximum * 360;
            Color baseColor = GetProgressColor(_animatedValue);

            // Glow
            using (Pen glow = new Pen(Color.FromArgb(60, baseColor.R, baseColor.G, baseColor.B), 18))
                e.Graphics.DrawArc(glow, rect, -90, progressAngle);

            // Background ring
            using (Pen bg = new Pen(Color.FromArgb(45, 45, 45), 14))
                e.Graphics.DrawArc(bg, rect, -90, 360);

            // Main ring
            using (Pen pen = new Pen(baseColor, 14) { EndCap = LineCap.Round, StartCap = LineCap.Round })
                e.Graphics.DrawArc(pen, rect, -90, progressAngle);

            // Center text
            using (Font font = new Font("Segoe UI", 24F, FontStyle.Bold))
            using (SolidBrush brush = new SolidBrush(ForeColor))
            {
                StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                e.Graphics.DrawString(_centerText, font, brush, rect, sf);
            }
        }

        public Color GetProgressColor(int percent)
        {
            if (percent <= 50)
            {
                int r = 0;
                int g = (int)(122 + (percent / 50f) * 133);
                int b = (int)(255 - (percent / 50f) * 100);
                return Color.FromArgb(r, g, b);
            }
            else if (percent <= 75)
            {
                float t = (percent - 50) / 25f;
                int r = (int)(t * 255);
                int g = 255;
                int b = (int)(155 - t * 155);
                return Color.FromArgb(r, g, b);
            }
            else
            {
                float t = (percent - 75) / 25f;
                int r = 255;
                int g = (int)(255 - t * 205);
                int b = 0;
                return Color.FromArgb(r, g, b);
            }
        }

        public void UpdateFromRAM(int usedPercent, string text)
        {
            CenterText = text;
            Value = usedPercent;
        }
    }
}
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MemoryCleaner
{
    public enum OverlayPosition
    {
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight
    }

    public sealed partial class RamOverlayForm : Form
    {
        private readonly RAMcs _ram = new();
        private readonly Label _label;
        private readonly System.Windows.Forms.Timer _timer;
        private OverlayPosition _position = OverlayPosition.TopRight;

        private const int Margin = 6;
        private const int WS_EX_LAYERED = 0x80000;
        private const int WS_EX_TRANSPARENT = 0x20;
        private const int WS_EX_TOOLWINDOW = 0x80;
        private const int WS_EX_NOACTIVATE = 0x08000000;
        private const int WS_EX_TOPMOST = 0x00000008;

        public RamOverlayForm()
        {
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            TopMost = true;
            StartPosition = FormStartPosition.Manual;
            Size = new Size(148, 26);
            BackColor = Color.FromArgb(16, 16, 16);
            Opacity = 0.72;
            DoubleBuffered = true;

            _label = new Label
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI Semibold", 8.5f, FontStyle.Bold),
                Text = "RAM --%"
            };
            Controls.Add(_label);

            _timer = new System.Windows.Forms.Timer { Interval = 1000 };
            _timer.Tick += (_, _) => UpdateText();
        }

        // Click-through + no taskbar/alt-tab noise
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= WS_EX_LAYERED | WS_EX_TRANSPARENT | WS_EX_TOOLWINDOW | WS_EX_NOACTIVATE | WS_EX_TOPMOST;
                return cp;
            }
        }

        protected override bool ShowWithoutActivation => true;

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            ApplyPosition(_position);
            UpdateText();
            _timer.Start();
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (Visible)
            {
                ApplyPosition(_position);
                UpdateText();
                _timer.Start();
            }
            else
            {
                _timer.Stop();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
                return;
            }

            _timer.Stop();
            base.OnFormClosing(e);
        }

        public void SetEnabled(bool enabled)
        {
            if (enabled) Show();
            else Hide();
        }

        public void SetPosition(OverlayPosition position)
        {
            _position = position;
            if (IsHandleCreated)
                ApplyPosition(_position);
        }

        public void Toggle()
        {
            if (Visible) Hide();
            else Show();
        }

        private void ApplyPosition(OverlayPosition position)
        {
            Rectangle wa = Screen.PrimaryScreen?.WorkingArea
                ?? new Rectangle(0, 0, 1920, 1080);

            int x = position switch
            {
                OverlayPosition.TopLeft or OverlayPosition.BottomLeft => wa.Left + Margin,
                _ => wa.Right - Width - Margin
            };

            int y = position switch
            {
                OverlayPosition.TopLeft or OverlayPosition.TopRight => wa.Top + Margin,
                _ => wa.Bottom - Height - Margin
            };

            Location = new Point(x, y);
        }

        private void UpdateText()
        {
            try
            {
                float used = _ram.GetUsagePercent();
                ulong free = _ram.GetAvailableBytes();
                _label.Text = $"{used:F0}%  {FormatBytes(free)}";
            }
            catch
            {
                _label.Text = "RAM --%";
            }
        }

        private static string FormatBytes(ulong bytes)
        {
            const double KB = 1024d;
            const double MB = KB * 1024d;
            const double GB = MB * 1024d;

            if (bytes >= GB) return $"{bytes / GB:F1} GB";
            if (bytes >= MB) return $"{bytes / MB:F0} MB";
            return $"{bytes / KB:F0} KB";
        }
    }
}
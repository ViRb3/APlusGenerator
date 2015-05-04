using System;
using System.Drawing;
using System.Windows.Forms;

namespace APlusGenerator
{
    class WatermarkTextBox : TextBox
    {
        private Font _originalFont;
        private Boolean _watermarked;

        private Color _watermarkColor = Color.Gray;

        public Color WatermarkColor
        {
            get { return _watermarkColor; }
            set
            {
                _watermarkColor = value;
                this.Invalidate();
            }
        }

        private string _watermarkText = "Water Mark";

        public string WatermarkText
        {
            get { return _watermarkText; }
            set
            {
                _watermarkText = value;
                this.Invalidate();
            }
        }


        public WatermarkTextBox()
        {
            HookEvents();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            Toggle(null, null);
        }

        protected override void OnPaint(PaintEventArgs args)
        {
            var drawFont = new Font(this.Font.FontFamily, this.Font.Size, this.Font.Style, this.Font.Unit);
            var drawBrush = new SolidBrush(WatermarkColor);
            args.Graphics.DrawString((_watermarked ? WatermarkText : this.Text),
                drawFont, drawBrush, new PointF(0.0F, 0.0F));

            base.OnPaint(args);
        }

        private void HookEvents()
        {
            this.TextChanged += Toggle;
            this.LostFocus += Toggle;
            this.FontChanged += WaterMark_FontChanged;
        }

        private void Toggle(object sender, EventArgs args)
        {
            if (this.Text.Length <= 0)
                DoWatermark();
            else
                DeWatermark();
        }

        private void DoWatermark()
        {
            _originalFont = new Font(this.Font.FontFamily, this.Font.Size, this.Font.Style, this.Font.Unit);
            this.SetStyle(ControlStyles.UserPaint, true);

            _watermarked = true;
            this.Refresh();
        }

        private void DeWatermark()
        {
            this.SetStyle(ControlStyles.UserPaint, false);

            if (_originalFont != null)
                this.Font = new Font(_originalFont.FontFamily, _originalFont.Size, _originalFont.Style, _originalFont.Unit);

            _watermarked = false;
        }

        private void WaterMark_FontChanged(object sender, EventArgs args)
        {
            if (_watermarked)
            {
                _originalFont = new Font(this.Font.FontFamily, this.Font.Size, this.Font.Style, this.Font.Unit);
                this.Refresh();
            }
        }
    }
}
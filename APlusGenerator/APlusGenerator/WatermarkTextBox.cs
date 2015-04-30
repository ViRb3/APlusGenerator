using System;
using System.Drawing;
using System.Windows.Forms;

namespace APlusGenerator
{
    class WatermarkTextBox : TextBox
    {
        private string _watermarkText = "Enter text here";
        private bool _waterMarked;

        private Color _originalColor;
        private bool _originalItalic;

        public WatermarkTextBox()
        {
            if (this.Text == string.Empty)
                Watermark();
        }

        public string WatermarkText
        {
            get { return _watermarkText; }
            set { _watermarkText = value; }
        }

        protected override void OnGotFocus(EventArgs e)
        {
            if (_waterMarked)
                DeWatermark();

            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            if (this.Text == string.Empty)
                Watermark();

            base.OnLostFocus(e);
        }

        private void Watermark()
        {
            _originalColor = this.ForeColor;
            _originalItalic = this.Font.Italic;

            if (!_originalItalic)
                this.Font = new Font(this.Font.FontFamily, this.Font.Size, this.Font.Style | FontStyle.Italic, this.Font.Unit, this.Font.GdiCharSet);

            this.ForeColor = Color.Gray;
            this.Text = _watermarkText;

            _waterMarked = true;
        }

        private void DeWatermark()
        {
            if (!_originalItalic)
                this.Font = new Font(this.Font.FontFamily, this.Font.Size, this.Font.Style & ~FontStyle.Italic, this.Font.Unit, this.Font.GdiCharSet);

            this.ForeColor = _originalColor;
            this.Text = string.Empty;

            _waterMarked = false;
        }
    }
}

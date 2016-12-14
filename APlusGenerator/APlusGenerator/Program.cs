using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace APlusGenerator
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }

        public static void ScaleForm(Form form)
        {
            using (Graphics g = form.CreateGraphics())
            {
                float sngScaleFactor = 1;

                if (g.DpiX > 96)
                    sngScaleFactor = g.DpiX / 96;

                if (form.AutoScaleDimensions != form.CurrentAutoScaleDimensions)
                    return;

                form.Scale(new SizeF(sngScaleFactor, sngScaleFactor));
                foreach (Control control in form.Controls)
                {
                    Font newFont = new Font(control.Font.FontFamily, control.Font.Size * sngScaleFactor, control.Font.Style, control.Font.Unit, control.Font.GdiCharSet);
                    control.Font = newFont;
                }
            }
        }

        public static void SizeListViewColumns(ListView listView)
        {
            listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }
    }
}
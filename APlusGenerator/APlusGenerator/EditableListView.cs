using System;
using System.Drawing;
using System.Windows.Forms;

namespace APlusGenerator
{
    internal class EditableListView : ListView
    {
        private const int WM_HSCROLL = 0x114;
        private const int WM_VSCROLL = 0x115;
        private readonly TextBox _txtListEdit;
        private Point _dragPoint;
        private ListViewItem.ListViewSubItem _selectedItem;
        private bool _updating;

        public EditableListView(TextBox editTextBox)
        {
            _txtListEdit = editTextBox;
        }

        private void HideTextEditor()
        {
            _txtListEdit.Visible = false;

            if (_selectedItem != null)
                _selectedItem.Text = _txtListEdit.Text;

            _selectedItem = null;
            _txtListEdit.Text = string.Empty;

            DeselectAll();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || this.SelectedItems.Count > 1)
                return;

            if (_dragPoint == default(Point) || _dragPoint != new Point(e.X, e.Y))
                return;

            ListViewHitTestInfo selectedItemInfo = this.HitTest(e.X, e.Y);
            _selectedItem = selectedItemInfo.SubItem;

            if (_selectedItem == null)
                return;

            _updating = true;

            var border = 0;
            switch (this.BorderStyle)
            {
                case BorderStyle.FixedSingle:
                    border = 1;
                    break;
                case BorderStyle.Fixed3D:
                    border = 2;
                    break;
            }

            int cellWidth = _selectedItem.Bounds.Width;
            int cellHeight = _selectedItem.Bounds.Height;
            int cellLeft = border + this.Left + selectedItemInfo.SubItem.Bounds.Left;
            int cellTop = this.Top + selectedItemInfo.SubItem.Bounds.Top;

            if (selectedItemInfo.SubItem == selectedItemInfo.Item.SubItems[0])
                cellWidth = this.Columns[0].Width;

            _txtListEdit.Location = new Point(cellLeft, cellTop);
            _txtListEdit.Size = new Size(cellWidth, cellHeight);
            _txtListEdit.Visible = true;
            _txtListEdit.BringToFront();
            _txtListEdit.Text = selectedItemInfo.SubItem.Text;
            _txtListEdit.Select();
            _txtListEdit.SelectAll();

            _updating = false;

            base.OnMouseUp(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            _dragPoint = new Point(e.X, e.Y);

            if (!_updating)
                HideTextEditor();

            base.OnMouseDown(e);
        }

        public event EventHandler Scroll;

        protected void OnScroll()
        {
            if (Scroll != null)
            {
                if (!_updating)
                    HideTextEditor();

                Scroll(this, EventArgs.Empty);
            }
        }

        protected override void WndProc(ref Message message)
        {
            base.WndProc(ref message);

            if (message.Msg == WM_HSCROLL || message.Msg == WM_VSCROLL)
                OnScroll();
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (!_updating)
                HideTextEditor();

            base.OnKeyUp(e);
        }

        protected override void OnLeave(EventArgs e)
        {
            if (!_updating)
                HideTextEditor();

            base.OnLeave(e);
        }

        private void DeselectAll()
        {
            foreach (ListViewItem item in this.Items)
                item.Selected = false;
        }
    }
}
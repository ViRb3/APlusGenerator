using System;
using System.Drawing;
using System.Windows.Forms;

namespace APlusGenerator
{
    internal class EditableListView : ListView
    {
        private const int WM_HSCROLL = 0x114;
        private const int WM_VSCROLL = 0x115;

        public TextBox TxtListEdit
        {
            get { return ManageAccountsForm.TxtListEdit; }
        }

        private Point _dragPoint;
        private ListViewItem.ListViewSubItem _selectedItem;
        private bool _updating;

        public EditableListView()
        {
            this.View = View.Details;
        }

        private void HideTextEditor()
        {
            TxtListEdit.Visible = false;

            if (_selectedItem != null)
                _selectedItem.Text = TxtListEdit.Text;

            _selectedItem = null;
            TxtListEdit.Text = string.Empty;

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

            TxtListEdit.Location = new Point(cellLeft, cellTop);
            TxtListEdit.Size = new Size(cellWidth, cellHeight);
            TxtListEdit.Visible = true;
            TxtListEdit.BringToFront();
            TxtListEdit.Text = selectedItemInfo.SubItem.Text;
            TxtListEdit.Select();
            TxtListEdit.SelectAll();

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
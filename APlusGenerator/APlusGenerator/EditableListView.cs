using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace APlusGenerator
{
    internal class EditableListView : ListView
    {
        private const int WM_HSCROLL = 0x114;
        private const int WM_VSCROLL = 0x115;
        private Point _dragPoint;
        private EventHandler _lostFocusHandler;
        private string[,] _originalData;
        private ListViewItem.ListViewSubItem _selectedItem;
        private bool _updating;

        public EditableListView()
        {
            this.View = View.Details;
        }

        public TextBox TxtListEdit
        {
            get { return ManageAccountsForm.TxtListEdit; }
        }

        public void SaveCurrentData()
        {
            if (this.Items.Count < 1)
                return;

            _originalData =
                new string[this.Items.Count, (from ListViewItem item in this.Items select item.SubItems.Count).Max()];

            for (var i = 0; i < this.Items.Count; i++)
            {
                ListViewItem item = this.Items[i];

                for (var o = 0; o < item.SubItems.Count; o++)
                {
                    ListViewItem.ListViewSubItem subItem = item.SubItems[o];
                    _originalData[i, o] = subItem.Text;
                }
            }
        }

        public string[,] GetSavedDataChanges()
        {
            var deltaData = new string[_originalData.GetUpperBound(0) + 1, _originalData.GetUpperBound(1) + 1];

            for (var i = 0; i < this.Items.Count; i++)
            {
                ListViewItem item = this.Items[i];

                for (var o = 0; o < item.SubItems.Count; o++)
                {
                    ListViewItem.ListViewSubItem subItem = item.SubItems[o];

                    if (subItem.Text != _originalData[i, o])
                        deltaData[i, o] = subItem.Text;
                }
            }

            return deltaData;
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

            if (_lostFocusHandler == null)
            {
                _lostFocusHandler = delegate { HideTextEditor(); };

                TxtListEdit.LostFocus += _lostFocusHandler;
            }

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
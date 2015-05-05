using System;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace APlusGenerator
{
    [Flags]
    public enum ViewType
    {
        Teacher = 0x1,
        Student = 0x2,
        Unactivated = 0x4
    }

    public partial class ManageAccountsForm : Form
    {
        private ViewType _typeView = ViewType.Teacher | ViewType.Student;

        public static TextBox TxtListEdit;

        public ManageAccountsForm()
        {
            InitializeComponent();
            TxtListEdit = txtListViewEdit;
        }

        private void ListAccounts()
        {
            var data = new NameValueCollection();
            data.Add("getaccounts", "");

            string @class = txtClass.Text.Trim();

            if (!string.IsNullOrWhiteSpace(@class))
                data.Add("class", @class);

            if (_typeView.HasFlag(ViewType.Student) && !_typeView.HasFlag(ViewType.Teacher))
                data.Add("type", "student");
            else if (_typeView.HasFlag(ViewType.Teacher) && !_typeView.HasFlag(ViewType.Student))
                data.Add("type", "teacher");

            if (_typeView.HasFlag(ViewType.Unactivated))
                data.Add("unactivatedonly", "");

            string reply = WebFunctions.Request(data);
            string[] accounts = Regex.Split(reply, "\n").TrimArray();

            editableListViewAccounts.BeginUpdate();

            editableListViewAccounts.Items.Clear();

            foreach (string account in accounts)
            {
                string[] accountInfo = Regex.Split(account, " ");

                if (accountInfo.Length != 6 || !account.Contains("@"))
                {
                    MessageBox.Show(reply, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                editableListViewAccounts.Items.Add(new ListViewItem(new[] { accountInfo[0], accountInfo[1], accountInfo[2], accountInfo[3], accountInfo[4], accountInfo[5] }));

                foreach (ColumnHeader column in editableListViewAccounts.Columns)
                    column.Width = -2;
            }

            editableListViewAccounts.EndUpdate();
        }

        private void btnNote_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                @"Letters from different keyboard layouts that look the same are recognized as different!
('A' from the English keyboard does not equal 'А' from the Bulgarian keyboard, etc.)", "Note",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void chkViewStudents_CheckedChanged(object sender, EventArgs e)
        {
            if (chkViewStudents.Checked)
                _typeView |= ViewType.Student;
            else
                _typeView &= ~ViewType.Student;
        }

        private void chkViewTeachers_CheckedChanged(object sender, EventArgs e)
        {
            if (chkViewTeachers.Checked)
                _typeView |= ViewType.Teacher;
            else
                _typeView &= ~ViewType.Teacher;
        }

        private void chkViewUnactivated_CheckedChanged(object sender, EventArgs e)
        {
            if (chkViewUnactivated.Checked)
                _typeView |= ViewType.Unactivated;
            else
                _typeView &= ~ViewType.Unactivated;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ListAccounts();
            editableListViewAccounts.SaveCurrentData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            string[,] deltaData = editableListViewAccounts.GetSavedDataChanges();
        }
    }
}

using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text;
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

            Program.ScaleForm(this);
            Program.SizeListViewColumns(editableListViewAccounts);
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
            if (editableListViewAccounts.Items.Count > 0)
            {
                if (MessageBox.Show("Any unsynced changes will be lost! Are you sure you want to continue?", "Warning",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                {
                    return;
                }
            }

            var refreshWorker = new BackgroundWorker();
            refreshWorker.DoWork += RefreshWorkerOnDoWork;
            refreshWorker.RunWorkerCompleted += RefreshWorkerOnRunWorkerCompleted;

            btnRefresh.Enabled = false;
            refreshWorker.RunWorkerAsync();          
        }

        private void RefreshWorkerOnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs runWorkerCompletedEventArgs)
        {
            object[] data = (object[]) runWorkerCompletedEventArgs.Result;
            string[] accounts = (string[]) data[0];
            string reply = (string) data[1];

            editableListViewAccounts.BeginUpdate();

            editableListViewAccounts.Items.Clear();

            foreach (string account in accounts)
            {
                string[] accountInfo = Regex.Split(account, " ");

                if (accountInfo.Length != 6 || !account.Contains("@"))
                {
                    MessageBox.Show(reply, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }

                editableListViewAccounts.Items.Add(new ListViewItem(new[] { accountInfo[0], accountInfo[1], accountInfo[2], accountInfo[3], accountInfo[4], accountInfo[5] }));

                foreach (ColumnHeader column in editableListViewAccounts.Columns)
                    column.Width = -2;
            }

            editableListViewAccounts.EndUpdate();

            editableListViewAccounts.SaveCurrentData();
            btnRefresh.Enabled = true;
        }

        private void RefreshWorkerOnDoWork(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            var data = new NameValueCollection();
            data.Add("getaccounts", "");

            string @class = txtClass.Text.ToUpper().Trim();

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

            doWorkEventArgs.Result = new object[] { accounts, reply };
        }

        private void btnClose_Click(object sender, EventArgs e)
        {      
            this.Close();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            string[,] deltaData = editableListViewAccounts.GetSavedDataChanges();

            if (deltaData == null)
            {
                MessageBox.Show("Nothing to apply!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Are you sure you want to sync the current changes to the server?", "Warning",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }

            StringBuilder updateData = new StringBuilder();

            for (int row = 0; row <= deltaData.GetUpperBound(0); row++)
                for (int column = 0; column <= deltaData.GetUpperBound(1); column++)
                {
                    if (deltaData[row, column] == null)
                        continue;

                    string email = editableListViewAccounts.Items[row].SubItems[0].Text;    
                    string newValue = editableListViewAccounts.Items[row].SubItems[column].Text;

                    updateData.Append(string.Format("{0}-|-{1}-|-{2}", email, column, newValue));
                    updateData.Append("-||-");
                }

            string reply = Post(updateData.ToString());

            if (reply == "Records updated!")
            {
                MessageBox.Show("Changes successfully applied!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                editableListViewAccounts.SaveCurrentData();
            }
            else
                MessageBox.Show(reply, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private string Post(string newData)
        {
            var data = new NameValueCollection();
            data.Add("updateaccounts", "");
            data.Add("data", newData);

            return WebFunctions.Request(data);
        }

        private void ManageAccountsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (editableListViewAccounts.Items.Count < 1)
                return;

            if (MessageBox.Show("Anything you haven't synced with the server will be lost! Are you sure you continue?", "Warning",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                e.Cancel = true;
            }
        }
    }
}

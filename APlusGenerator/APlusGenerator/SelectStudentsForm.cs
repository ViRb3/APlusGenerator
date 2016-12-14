using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace APlusGenerator
{
    public partial class SelectStudentsForm : Form
    {
        private readonly MainForm _mainForm;

        public SelectStudentsForm(MainForm mainForm)
        {
            _mainForm = mainForm;

            InitializeComponent();
            Program.ScaleForm(this);
            Program.SizeListViewColumns(listViewStudents);
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewStudents.Items)
                item.Checked = true;
        }

        private void btnDeselectAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewStudents.Items)
                item.Checked = false;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewStudents.Items.Cast<ListViewItem>().Where(item => item.Checked))
            {
                _mainForm.Students.Add(new Student(item.SubItems[0].Text, item.SubItems[1].Text, item.SubItems[2].Text, item.SubItems[3].Text));
            }

            this.Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            var refreshWorker = new BackgroundWorker();
            refreshWorker.DoWork += refreshWorker_DoWork;
            refreshWorker.RunWorkerCompleted += refreshWorker_RunWorkerCompleted;

            btnRefresh.Enabled = false;
            refreshWorker.RunWorkerAsync();
        }

        void refreshWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            object[] result = (object[]) e.Result;
            string[] students = (string[])result[0];
            string reply = (string) result[1];

            listViewStudents.BeginUpdate();

            listViewStudents.Items.Clear();

            foreach (string student in students)
            {
                string[] studentInfo = Regex.Split(student, " ");

                if (studentInfo.Length != 4 || !student.Contains("@"))
                {
                    MessageBox.Show(reply, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                }

                listViewStudents.Items.Add(new ListViewItem(new[] { studentInfo[0], studentInfo[1], studentInfo[2], studentInfo[3] }));

                foreach (ColumnHeader column in listViewStudents.Columns)
                    column.Width = -2;
            }

            listViewStudents.EndUpdate();
            btnRefresh.Enabled = true;
        }

        void refreshWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var data = new NameValueCollection();
            data.Add("getstudents", "");

            string @class = txtClass.Text.ToUpper().Trim();

            if (!string.IsNullOrWhiteSpace(@class))
                data.Add("class", @class);

            string reply = WebFunctions.Request(data);
            string[] students = Regex.Split(reply, "\n").TrimArray();

            e.Result = new object[] {students, reply};
        }

        private void btnNote_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                @"Letters from different keyboard layouts that look the same are recognized as different!
('A' from the English keyboard does not equal 'А' from the Bulgarian keyboard, etc.)", "Note",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

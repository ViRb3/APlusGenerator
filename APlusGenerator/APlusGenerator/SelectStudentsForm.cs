using System;
using System.Collections.Specialized;
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
            ListStudents();
        }

        private void ListStudents()
        {
            var data = new NameValueCollection();
            data.Add("getstudents", "");

            string reply = WebFunctions.Request(data);
            string[] students = Regex.Split(reply, Environment.NewLine);

            foreach (string student in students)
            {
                string[] studentInfo = Regex.Split(student, " ");

                if (studentInfo.Length != 4 || !student.Contains("@"))
                {
                    MessageBox.Show(reply, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                listViewStudents.Items.Add(new ListViewItem(new[] { studentInfo[0], studentInfo[1], studentInfo[2], studentInfo[3] }));

                foreach (ColumnHeader column in listViewStudents.Columns)
                    column.Width = -2;
            }
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
    }
}

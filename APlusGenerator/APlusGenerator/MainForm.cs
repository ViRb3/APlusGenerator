using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APlusGenerator
{
    public partial class MainForm : Form
    {
        public List<Student> Students = new List<Student>();

        public MainForm()
        {
            InitializeComponent();
            Program.ScaleForm(this);
            Program.SizeListViewColumns(listViewStudents);
        }

        private void btnSelectStudents_Click(object sender, EventArgs e)
        {
            new SelectStudentsForm(this).ShowDialog();
            UpdateStudentsList();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            groupWelcome.Enabled = false;
            btnGenerate.Enabled = false;

            new TaskFactory().StartNew(() =>
            {
                try
                {
                    var folderDialog = new FolderBrowserDialog();

                    if (!(bool) this.Invoke(new Func<bool>(() => folderDialog.ShowDialog() == DialogResult.OK)))
                        return;

                    string folder = folderDialog.SelectedPath;

                    var generator = new Generator(Students);
                    int codesCount = (int) numCodes.Invoke(new Func<int>(() => Convert.ToInt32(numCodes.Value)));

                    Tuple<Student, Bitmap[]>[] codes = generator.Generate(codesCount);

                    foreach (Tuple<Student, Bitmap[]> studentCodes in codes)
                    {
                        string newFolder = Path.Combine(folder, string.Format("{0} {1} {2}", studentCodes.Item1.FirstName, 
                            studentCodes.Item1.LastName, studentCodes.Item1.Class));

                        Directory.CreateDirectory(newFolder);

                        for (int i = 0; i < studentCodes.Item2.Length; i++)
                        {
                            var bitmap = studentCodes.Item2[i];
                            bitmap.Save(Path.Combine(newFolder, string.Format("{0}.bmp", i)));
                        }
                    }

                    MessageBox.Show("All done!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {
                    this.BeginInvoke((MethodInvoker) delegate
                    {
                        groupWelcome.Enabled = true;
                        btnGenerate.Enabled = true;
                    });
                }
            });
        }

        public void UpdateStudentsList()
        {
            if (listViewStudents.Items.Count == 0 && Students.Count == 0)
                return;

            listViewStudents.BeginUpdate();

            while (listViewStudents.Items.Count > 0)
                listViewStudents.Items.RemoveAt(0);

            foreach (var student in Students)
                if (listViewStudents.Items.Cast<ListViewItem>().Count(item => item.SubItems[0].Text == student.EMail) == 0)
                    listViewStudents.Items.Add(new ListViewItem(new[] {student.EMail, student.FirstName, student.LastName, student.Class}));

            foreach (ColumnHeader column in listViewStudents.Columns)
                column.Width = -2;

            listViewStudents.EndUpdate();
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewStudents.SelectedItems.Count < 1)
                return;

            foreach (ListViewItem item in listViewStudents.SelectedItems)
            {
                Students.RemoveAt(item.Index);
                item.Remove();
            }
        }

        private void btnManageAccounts_Click(object sender, EventArgs e)
        {
            new ManageAccountsForm().ShowDialog();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (listViewStudents.Items.Count < 1)
                return;

            if (MessageBox.Show("Are you sure you want to exit the application?", "Warning",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                e.Cancel = true;
            }
        }
    }
}
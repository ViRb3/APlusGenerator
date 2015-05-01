using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
        }

        private void btnSelectStudents_Click(object sender, EventArgs e)
        {
            new SelectStudentsForm(this).ShowDialog();
            UpdateStudentsList();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            btnSelectStudents.Enabled = false;
            btnGenerate.Enabled = false;

            try
            {
                var taskFactory = new TaskFactory();
                taskFactory.StartNew(() =>
                {
                    var folderDialog = new FolderBrowserDialog();
                    int resultState = 0;

                    this.BeginInvoke(new MethodInvoker(() =>
                    {
                        if (folderDialog.ShowDialog() != DialogResult.OK || !Directory.Exists(folderDialog.SelectedPath))
                            resultState = -1;
                        else
                            resultState = 1;
                    }));

                    while (resultState == 0)
                        Thread.Sleep(100);

                    if (resultState == -1)
                        return;

                    string folder = folderDialog.SelectedPath;

                    var generator = new Generator(Students);
                    int codesCount = -1;
                    numCodes.BeginInvoke(new MethodInvoker(() => codesCount = Convert.ToInt32(numCodes.Value)));

                    while (codesCount == -1)
                        Thread.Sleep(100);

                    Tuple<Student, Bitmap[]>[] codes = generator.Generate(codesCount);

                    foreach (Tuple<Student, Bitmap[]> studentCodes in codes)
                    {
                        string newFolder = Path.Combine(folder, studentCodes.Item1.EMail);

                        Directory.CreateDirectory(newFolder);

                        for (int i = 0; i < studentCodes.Item2.Length; i++)
                        {
                            var bitmap = studentCodes.Item2[i];
                            bitmap.Save(Path.Combine(newFolder, string.Format("{0}.bmp", i)));
                        }
                    }

                    MessageBox.Show("All done!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                });
            }
            finally
            {
                btnSelectStudents.Enabled = true;
                btnGenerate.Enabled = true;
            }    
        }

        public void UpdateStudentsList()
        {
            listViewStudents.BeginUpdate();

            while (listViewStudents.Items.Count > 0)
                listViewStudents.Items.RemoveAt(0);

            foreach (var student in Students)
                listViewStudents.Items.Add(new ListViewItem(new[] { student.EMail, student.FirstName, student.LastName, student.Class }));

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
    }
}

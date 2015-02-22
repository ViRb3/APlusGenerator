using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APlusGenerator
{
    public partial class MainForm : Form
    {
        private readonly List<Student> _students = new List<Student>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnGenerateFromFile_Click(object sender, EventArgs e)
        {
            //TODO: Add threaded loading
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files|*.txt";
            openFileDialog.Title = "Select list to load...";

            if (openFileDialog.ShowDialog() != DialogResult.OK || !openFileDialog.CheckFileExists)
                return;

            string[] list = Regex.Split(File.ReadAllText(openFileDialog.FileName).Trim(), Environment.NewLine);

            try
            {
                foreach (string line in list)
                _students.Add(new Student(line));
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            UpdateStudentsList();
        }

        private void btnGenerateSingle_Click(object sender, EventArgs e)
        {
            //TODO: Form for single student
        }

        private void UpdateStudentsList()
        {
            listViewStudents.BeginUpdate();

            while (listViewStudents.Items.Count > 0)
                listViewStudents.Items.RemoveAt(0);

            foreach (var student in _students)
                listViewStudents.Items.Add(new ListViewItem(new[] {student.FirstName, student.LastName, student.Class}));

            listViewStudents.EndUpdate();
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            btnGenerateFromFile.Enabled = false;
            btnGenerateSingle.Enabled = false;
            btnGenerate.Enabled = false;

            try
            {
                var taskFactory = new TaskFactory();
                taskFactory.StartNew(() =>
                {
                    var folderDialog = new FolderBrowserDialog();
                    int state = 0;

                    this.BeginInvoke(new MethodInvoker(() =>
                    {
                        if (folderDialog.ShowDialog() != DialogResult.OK || !Directory.Exists(folderDialog.SelectedPath))
                            state = -1;
                        else
                            state = 1;
                    }));

                    while (state == 0)
                        Thread.Sleep(100);

                    if (state == -1)
                        return;

                    string folder = folderDialog.SelectedPath;

                    var generator = new Generator(_students);
                    int codesCount = -1;
                    numCodes.BeginInvoke(new MethodInvoker(() => codesCount = Convert.ToInt32(numCodes.Value)));

                    while (codesCount == -1)
                        Thread.Sleep(100);

                    Tuple<Student, Bitmap[]>[] codes = generator.Generate(codesCount);

                    foreach (Tuple<Student, Bitmap[]> studentCodes in codes)
                    {
                        string newFolder = Path.Combine(folder,
                            string.Format("{0}_{1}_{2}", studentCodes.Item1.FirstName, studentCodes.Item1.LastName,
                                studentCodes.Item1.Class));

                        Directory.CreateDirectory(newFolder);

                        for (int i = 0; i < studentCodes.Item2.Length; i++)
                        {
                            var bitmap = studentCodes.Item2[i];
                            bitmap.Save(Path.Combine(newFolder, string.Format("{0}.bmp", i)));
                        }
                    }

                    MessageBox.Show("All done!");
                });
            }
            finally
            {
                btnGenerateFromFile.Enabled = true;
                btnGenerateSingle.Enabled = true;
                btnGenerate.Enabled = true;
            }    
        }
    }
}

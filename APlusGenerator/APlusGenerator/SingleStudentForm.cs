using System;
using System.Windows.Forms;

namespace APlusGenerator
{
    public partial class SingleStudentForm : Form
    {
        private readonly MainForm _mainForm;

        public SingleStudentForm(MainForm mainForm)
        {
            InitializeComponent();
            _mainForm = mainForm;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var student = new Student(txtEmail.Text.Trim());
                _mainForm.Students.Add(student);
                _mainForm.UpdateStudentsList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.Close();
        }
    }
}

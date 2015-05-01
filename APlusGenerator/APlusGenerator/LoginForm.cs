using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Forms;

namespace APlusGenerator
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += Login;
            backgroundWorker.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;

            btnLogin.Enabled = false;
            backgroundWorker.RunWorkerAsync();
        }

        void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnLogin.Enabled = true;
        }

        private void Login(object sender, DoWorkEventArgs e)
        {
            var data = new NameValueCollection();
            data.Add("login", string.Empty);
            data.Add("email", txtEmail.Text);
            data.Add("password", Functions.GetSha256(txtPassword.Text));

            string reply = WebFunctions.Request(data);

            if (reply != "Login success!")
            {
                MessageBox.Show(reply, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Cleanup();
                return;
            }

            data.Clear();
            data.Add("getaccounttype", string.Empty);

            reply = WebFunctions.Request(data);

            if (reply != "admin")
            {
                MessageBox.Show("Account is not administrator!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Cleanup();
                return;
            }

            this.BeginInvoke(new MethodInvoker(delegate
            {
                var mainForm = new MainForm();
                this.Owner = mainForm;
                this.Hide();

                mainForm.ShowDialog();
            }));
        }

        private void Cleanup()
        {
            this.BeginInvoke(new MethodInvoker(() => txtPassword.Text = string.Empty));
            WebFunctions.ClearCookies();
        }
    }
}

using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CourseProject
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showPswd_CheckedChanged(object sender, EventArgs e)
        {
            pswdBox.PasswordChar = !showPswd.Checked ? '*' : char.MinValue;
        }

        private void logIn_Click(object sender, EventArgs e)
        {
            string usrInfo = $" Persist Security Info=False;User Id={lgBox.Text};Password={pswdBox.Text};";
            SqlConnection con = new SqlConnection(MainForm.ConnectionString + usrInfo);

            bool canProceed = false;
            try
            {
                con.Open();
                canProceed = true;
                con.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Введены неверные данные\n" + ex.Message);
            }

            if (canProceed)
            {
                this.Hide();
                var f = new MainForm();
                f.UserInfo = usrInfo;
                f.Show();
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
    }
}

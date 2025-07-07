using System;
using System.Windows.Forms;
using ConvertSuhu.Controllers;

namespace ConvertSuhu.Views
{
    public partial class Form1 : Form
    {
        public static int LoggedInUserId;

        public Form1()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Register registerForm = new Register();
            registerForm.Show();
            this.Hide();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void LOGIN_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;

            if (!string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(password))
            {
                if (AuthController.Login(email, password, out int userId))
                {
                    LoggedInUserId = userId;
                    HOME homeForm = new HOME(userId);
                    homeForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Email atau password salah!", "Login Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Mohon isi email dan password.", "Login Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}

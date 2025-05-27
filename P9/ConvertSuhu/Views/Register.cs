using System;
using System.Windows.Forms;
using ConvertSuhu.Controllers;

namespace ConvertSuhu.Views
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();
            string email = textBox2.Text.Trim();
            string password = textBox3.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Semua field harus diisi.");
                return;
            }

            if (AuthController.Register(username, email, password))
            {
                MessageBox.Show("Registrasi berhasil! Silakan login.");
                Form1 loginForm = new Form1();
                loginForm.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Email sudah digunakan, gunakan email lain.");
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 loginForm = new Form1();
            loginForm.Show();
            this.Close();
        }
    }
}
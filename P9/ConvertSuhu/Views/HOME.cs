using System;
using System.Windows.Forms;
using ConvertSuhu.Controllers;
using ConvertSuhu.Models;


namespace ConvertSuhu.Views
{
    public partial class HOME : Form
    {
        private readonly HomeController homeController;

        public int UserId { get; }

        public HOME()
        {
            InitializeComponent();
            homeController = new HomeController();
        }

        public HOME(int userId)
        {
            InitializeComponent();
            homeController = new HomeController();
            UserId = userId;
        }

        private void HOME_Load(object sender, EventArgs e)
        {
            comboBox1.Items.AddRange(new string[] { "Celcius", "Fahrenheit", "Reamur", "Kelvin" });
            comboBox2.Items.AddRange(new string[] { "Celcius", "Fahrenheit", "Reamur", "Kelvin" });

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 1;
        }

        private void LOGIN_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null || comboBox2.SelectedItem == null || string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Harap lengkapi semua input konversi suhu.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string fromUnit = comboBox1.SelectedItem.ToString();
            string toUnit = comboBox2.SelectedItem.ToString();

            if (!double.TryParse(textBox3.Text, out double inputValue))
            {
                MessageBox.Show("Input harus berupa angka.", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            double result = homeController.Konversi(inputValue, fromUnit, toUnit);

            textBox4.Text = result.ToString("F2");

            int userId = Form1.LoggedInUserId;
            homeController.SimpanRiwayat(userId, inputValue, fromUnit, toUnit, result);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            History historyForm = new History(UserId);
            historyForm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EditProfil editForm = new EditProfil(UserId);
            editForm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Apakah Anda yakin ingin keluar?", "Konfirmasi Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Form1.LoggedInUserId = -1;
                Form1 loginForm = new Form1();
                loginForm.Show();
                this.Hide();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.') ||
                (e.KeyChar == '.' && textBox3.Text.Contains(".")))
            {
                e.Handled = true;
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

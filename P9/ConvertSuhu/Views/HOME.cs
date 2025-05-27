using System;
using System.Windows.Forms;
using ConvertSuhu.Controllers;

namespace ConvertSuhu.Views
{
    public partial class HOME : Form
    {
        private readonly string[] comboOptions = { "Celcius", "Fahrenheit", "Kelvin", "Reamur" };
        private bool isUpdatingCombo = false;
        private int userId;

        public HOME(int userId)
        {
            InitializeComponent();
            this.userId = userId;
        }

        private void HOME_Load(object sender, EventArgs e)
        {
            comboBox1.Items.AddRange(comboOptions);
            comboBox2.Items.AddRange(comboOptions);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isUpdatingCombo || comboBox1.SelectedItem == null) return;
            isUpdatingCombo = true;
            RefreshComboBox(comboBox1, comboBox2);
            isUpdatingCombo = false;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isUpdatingCombo || comboBox2.SelectedItem == null) return;
            isUpdatingCombo = true;
            RefreshComboBox(comboBox2, comboBox1);
            isUpdatingCombo = false;
        }

        private void RefreshComboBox(ComboBox changed, ComboBox target)
        {
            var selected = changed.SelectedItem.ToString();
            var previous = target.SelectedItem?.ToString();
            target.Items.Clear();
            foreach (var option in comboOptions)
                if (option != selected) target.Items.Add(option);
            if (previous != null && target.Items.Contains(previous))
                target.SelectedItem = previous;
            else
                target.SelectedIndex = -1;
        }

        private void LOGIN_Click(object sender, EventArgs e)
        {
            if (!double.TryParse(textBox3.Text, out double input))
            {
                MessageBox.Show("Masukkan angka yang valid.");
                return;
            }

            string from = comboBox1.SelectedItem?.ToString();
            string to = comboBox2.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(from) || string.IsNullOrEmpty(to))
            {
                MessageBox.Show("Silakan pilih satuan suhu asal dan tujuan.");
                return;
            }

            double hasil = HomeController.Konversi(input, from, to);
            textBox4.Text = $"{hasil:F2} {to}";

            HistoryController.Simpan(userId, input, from, to, hasil);
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            textBox3.Text = "";
            textBox4.Text = "";
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox1.Items.AddRange(comboOptions);
            comboBox2.Items.AddRange(comboOptions);
        }

        private void buttonHistory_Click(object sender, EventArgs e)
        {
            History historyForm = new History(userId);
            historyForm.Show();
            this.Hide();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            EditProfil editForm = new EditProfil(userId);
            editForm.Show();
            this.Hide();
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Yakin ingin logout?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Form1 loginForm = new Form1();
                loginForm.Show();
                this.Close();
            }
        }
    }
}
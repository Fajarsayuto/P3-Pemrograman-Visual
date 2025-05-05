using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ConvertSuhu
{
    public partial class HOME : Form
    {
        private readonly string[] comboOptions = { "Celcius", "Fahrenheit", "Kelvin", "Reamur" };
        private bool isLoadingCombo = false;
        private bool isUpdatingCombo = false;
        private int userId;



        public HOME(int userId)
        {
            InitializeComponent();
            this.userId = userId;
        }

        public HOME() : this(0) { }


        private void HOME_Load(object sender, EventArgs e)
        {
            LoadComboBoxOptions();

        }

        private void LoadComboBoxOptions()
        {
            isLoadingCombo = true;

            comboBox1.Items.Clear();
            comboBox2.Items.Clear();

            comboBox1.Items.AddRange(comboOptions);
            comboBox2.Items.AddRange(comboOptions);

            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;

            isLoadingCombo = false;
        }

        private void RefreshComboBoxOptions(ComboBox changedComboBox, ComboBox targetComboBox)
        {
            if (changedComboBox.SelectedItem == null) return;

            string selectedItem = changedComboBox.SelectedItem.ToString();
            string previousSelection = targetComboBox.SelectedItem?.ToString();

            targetComboBox.Items.Clear();

            foreach (var option in comboOptions)
            {
                if (option != selectedItem)
                {
                    targetComboBox.Items.Add(option);
                }
            }

            if (previousSelection != null && targetComboBox.Items.Contains(previousSelection))
            {
                targetComboBox.SelectedItem = previousSelection;
            }
            else
            {
                targetComboBox.SelectedIndex = -1;
            }
        }

        private void ResetForm()
        {
            isLoadingCombo = true;

            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;

            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox1.Items.AddRange(comboOptions);
            comboBox2.Items.AddRange(comboOptions);

            textBox3.Text = "";
            textBox4.Text = "";

            isLoadingCombo = false;
        }



        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void LOGIN_Click(object sender, EventArgs e)
        {
            if (!double.TryParse(textBox3.Text, out double input))
            {
                MessageBox.Show("Masukkan angka yang valid untuk suhu.", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string from = comboBox1.SelectedItem?.ToString();
            string to = comboBox2.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(from) || string.IsNullOrEmpty(to))
            {
                MessageBox.Show("Silakan pilih satuan asal dan tujuan.", "Pilih Satuan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            double inCelcius = input;
            switch (from)
            {
                case "Fahrenheit": inCelcius = (input - 32) * 5 / 9; break;
                case "Kelvin": inCelcius = input - 273.15; break;
                case "Reamur": inCelcius = input * 5 / 4; break;
            }

            double result = inCelcius;
            switch (to)
            {
                case "Fahrenheit": result = (inCelcius * 9 / 5) + 32; break;
                case "Kelvin": result = inCelcius + 273.15; break;
                case "Reamur": result = inCelcius * 4 / 5; break;
            }

            textBox4.Text = $"{result:F2} {to}";
            SimpanKeDatabase(input, from, to, result);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Apakah Anda yakin ingin keluar?",
                "Konfirmasi Logout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Form1 loginForm = new Form1();
                loginForm.Show();
                this.Close();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            ResetForm();
        }


        private void SimpanKeDatabase(double suhuAwal, string dariSatuan, string keSatuan, double hasil)
        {
            string connectionString = "Server=localhost;Database=convertSuhu;Uid=root;Pwd=;";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = "INSERT INTO history (UserID, SuhuAwal, DariSatuan, KeSatuan, Hasil) " +
                               "VALUES (@UserID, @SuhuAwal, @DariSatuan, @KeSatuan, @Hasil)";

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    cmd.Parameters.AddWithValue("@SuhuAwal", suhuAwal);
                    cmd.Parameters.AddWithValue("@DariSatuan", dariSatuan);
                    cmd.Parameters.AddWithValue("@KeSatuan", keSatuan);
                    cmd.Parameters.AddWithValue("@Hasil", hasil);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Gagal menyimpan history: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }




        private void button5_Click(object sender, EventArgs e)
        {
            History historyForm = new History(Form1.LoggedInUserId);
            historyForm.Show();
            this.Hide();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isUpdatingCombo || comboBox1.SelectedItem == null) return;

            isUpdatingCombo = true;
            RefreshComboBoxOptions(comboBox1, comboBox2);
            isUpdatingCombo = false;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isUpdatingCombo || comboBox2.SelectedItem == null) return;

            isUpdatingCombo = true;
            RefreshComboBoxOptions(comboBox2, comboBox1);
            isUpdatingCombo = false;

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.' && (sender as TextBox).Text.Contains('.'))
            {
                e.Handled = true;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            EditProfil EditForm = new EditProfil (Form1.LoggedInUserId);
            EditForm.Show();
            this.Hide();
        }
    }
}

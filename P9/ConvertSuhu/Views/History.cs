using System;
using System.Windows.Forms;
using ConvertSuhu.Controllers;
using System.Data;

namespace ConvertSuhu.Views
{
    public partial class History : Form
    {
        private int userId;

        public History(int userId)
        {
            InitializeComponent();
            this.userId = userId;
        }

        private void History_Load(object sender, EventArgs e)
        {
            LoadUserHistory();
        }

        private void LoadUserHistory()
        {
            DataTable historyData = HistoryController.GetUserHistory(userId);
            dataGridView1.DataSource = historyData;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HOME homeForm = new HOME(userId);
            homeForm.Show();
            this.Close();
        }
    }
}
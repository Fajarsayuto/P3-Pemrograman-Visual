using MySql.Data.MySqlClient;

namespace ConvertSuhu
{
    public static class Database
    {
        private static readonly string connectionString = "Server=localhost;Database=convertSuhu;Uid=root;Pwd=;";

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}

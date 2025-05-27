using MySql.Data.MySqlClient;

namespace ConvertSuhu.Models
{
    public class Database
    {
        private static string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}
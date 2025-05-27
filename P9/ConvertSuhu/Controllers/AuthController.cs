using ConvertSuhu.Models;
using System.Windows.Forms;

namespace ConvertSuhu.Controllers
{
    public class AuthController
    {
        public static bool Login(string email, string password, out int userId)
        {
            return UserActivity.ValidateLogin(email, password, out userId);
        }

        public static bool Register(string username, string email, string password)
        {
            return UserActivity.RegisterUser(username, email, password);
        }
    }
}
using ConvertSuhu.Models;

namespace ConvertSuhu.Controllers
{
    public class UserController
    {
        public static bool UpdateProfile(int userId, string username, string email, string passwordHash)
        {
            return UserActivity.UpdateUser(userId, username, email, passwordHash);
        }

        public static User GetUserById(int userId)
        {
            return UserActivity.GetUserById(userId);
        }
    }
}
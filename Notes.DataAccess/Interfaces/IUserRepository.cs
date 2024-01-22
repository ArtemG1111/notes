

using Notes.DataAccess.Data.Models;

namespace Notes.DataAccess.Interfaces
{
    public interface IUserRepository
    {
        void Registration(User user);
        User? LogIn(User user);
        List<User> GetUsers();
    }
}

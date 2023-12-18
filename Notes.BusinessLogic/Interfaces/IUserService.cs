

using Notes.DataAccess.Data.Models;

namespace Notes.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        void Registration(User user);
        User LogIn(User user);
    }
}

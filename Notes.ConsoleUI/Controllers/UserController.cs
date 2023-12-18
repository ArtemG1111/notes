using Notes.BusinessLogic.Interfaces;
using Notes.DataAccess.Data.Models;

namespace Notes.ConsoleUI.Controllers
{
    public class UserController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        public void Registration(User user)
        {
            _userService.Registration(user);
        }
        public User LogIn(User user)
        {
            return _userService.LogIn(user);
        }
    }
}

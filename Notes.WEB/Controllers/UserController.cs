using Microsoft.AspNetCore.Mvc;
using Notes.BusinessLogic.Interfaces;
using Notes.DataAccess.Data.Models;

namespace Notes.WEB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        public void Registration(User user)
        {
            _userService.Registration(user);
        }
        [HttpGet]
        public User LogIn(User user)
        {
            return _userService.LogIn(user);
        }
    }
}

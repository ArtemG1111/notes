using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Notes.BusinessLogic.Interfaces;
using Notes.DataAccess.Data.Models;
using Notes.WEB.ViewModels;

namespace Notes.WEB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, IMapper userMapper, ILogger<UserController> logger)
        {
            _userService = userService;
            _mapper = userMapper;
            _logger = logger;
        }
        [HttpPost]
        public IActionResult Registration(UserViewModel user)
        {
            _userService.Registration(_mapper.Map<User>(user));
            _logger.LogInformation($"User was successfully added");
            return Ok($"User was successfully added");
        }
        [HttpPost]
        [Route("login")]
        public IActionResult LogIn(UserViewModel user)
        {
            _userService.LogIn(_mapper.Map<User>(user));
            _logger.LogInformation($"User was LogIn");
            return Ok($"User was LogIn");
        }
        [HttpGet]
        public List<User> GetUsers()
        {
            return _userService.GetUsers();
        }
    }
}

using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly IValidator<UserViewModel> _validator;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserController(IUserService userService, IMapper userMapper, ILogger<UserController> logger
            , IValidator<UserViewModel> validator, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userService = userService;
            _mapper = userMapper;
            _logger = logger;
            _validator = validator;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpPost]
        public async Task<IActionResult> Registration(UserViewModel user)
        {
            ValidationResult result = await _validator.ValidateAsync(user);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(e => new { Property = e.PropertyName, ErrorMessage = e.ErrorMessage });
                return BadRequest(new { Errors = errors });
            }

            var identityUser = _mapper.Map<User>(user);

            var registrationResult = await _userManager.CreateAsync(identityUser);

            if (!registrationResult.Succeeded) 
            {
                _logger.LogError($"Registration failed");

                return BadRequest("Invalid login or password");
            }

            await _signInManager.SignInAsync(identityUser, false);

            _logger.LogInformation($"User was successfully added");
            
            return Ok($"User was successfully added");
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LogIn(UserViewModel user)
        {
            ValidationResult result = await _validator.ValidateAsync(user);
            if (!result.IsValid)
            {
                var errors = result.Errors.Select(e => new { Property = e.PropertyName, ErrorMessage = e.ErrorMessage });
                return BadRequest(new { Errors = errors });
            }

            var loginResult = await _signInManager.PasswordSignInAsync(_mapper.Map<User>(user), user.Password, false, false);

            if (!loginResult.Succeeded)
            {
                _logger.LogError("Login failed");

                return BadRequest("Invalid login or password");
            }

            _logger.LogInformation($"User was LogIn");

            return Ok($"User was LogIn");
        }
        
        [HttpGet]
        [Authorize]
        public List<User> GetUsers()
        {
            return _userService.GetUsers();
        }

        [HttpPost("logout")]
        public async Task<IActionResult> LogOut() 
        {
            if (User.Identity?.Name == null) 
            {
                return BadRequest("You can't log out before log in");
            }

            await _signInManager.SignOutAsync();

            return Ok("Successfully Logged out");
        }
    }
}

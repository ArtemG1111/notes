using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
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
        private IValidator<UserViewModel> _validator;

        public UserController(IUserService userService, IMapper userMapper, ILogger<UserController> logger
            , IValidator<UserViewModel> validator)
        {
            _userService = userService;
            _mapper = userMapper;
            _logger = logger;
            _validator = validator;
        }
        [HttpPost]
        public IActionResult Registration(UserViewModel user)
        {
            ValidationResult result = _validator.Validate(user);
            if (!result.IsValid)
            {
                var errors = result.Errors.Select(e => new { Property = e.PropertyName, ErrorMessage = e.ErrorMessage });
                return BadRequest(new { Errors = errors });
            }
            _userService.Registration(_mapper.Map<User>(user));
            _logger.LogInformation($"User was successfully added");
            return Ok($"User was successfully added");
        }
        [HttpPost]
        [Route("login")]
        public IActionResult LogIn(UserViewModel user)
        {
            ValidationResult result = _validator.Validate(user);
            if (!result.IsValid)
            {
                var errors = result.Errors.Select(e => new { Property = e.PropertyName, ErrorMessage = e.ErrorMessage });
                return BadRequest(new { Errors = errors });
            }
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

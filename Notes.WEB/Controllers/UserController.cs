﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Notes.BusinessLogic.Interfaces;
using Notes.DataAccess.Data.Models;
using Notes.WEB.ViewModels;

namespace Notes.WEB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController
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
        public void Registration(UserViewModel user)
        {
            _userService.Registration(_mapper.Map<User>(user));
            _logger.LogInformation($"User was successfully added");
        }
        [HttpPost]
        [Route("login")]
        public User LogIn(UserViewModel user)
        {
            return _userService.LogIn(_mapper.Map<User>(user));
        }
        [HttpGet]
        public List<User> GetUsers()
        {
            return _userService.GetUsers();
        }
    }
}

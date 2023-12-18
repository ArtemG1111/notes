

using Notes.BusinessLogic.Interfaces;
using Notes.DataAccess.Data.Models;
using Notes.DataAccess.Interfaces;

namespace Notes.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public void Registration(User user)
        {
            _userRepository.Registration(user);           
        }
        public User LogIn(User user)
        {
            return _userRepository.LogIn(user);
        }
    }
}

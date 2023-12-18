
using Notes.DataAccess.Data;
using Notes.DataAccess.Data.Models;
using Notes.DataAccess.Interfaces;

namespace Notes.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly NotesContext _context;
        public UserRepository(NotesContext context)
        {
            _context = context;
        }
        public void Registration(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();            
        }
        public User? LogIn(User user)
        {
            return _context.Users.FirstOrDefault(u => u.UserName == user.UserName
            && u.Password == user.Password);
        }
    }
}

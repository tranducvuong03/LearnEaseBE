using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.IRepo;
using Repository.Model;

namespace Repository.Repo
{
    public class UserRepository : IUserRepository
    {
        private readonly Summer2025handbagdbContext _context;
        public UserRepository(Summer2025handbagdbContext context) => _context = context;

        public User GetByEmailPassword(string email, string password)
        {
            return _context.Users.FirstOrDefault(u =>
         u.Email.Trim().ToLower() == email.Trim().ToLower()
         && u.Password == password.Trim());
        }
    }

}

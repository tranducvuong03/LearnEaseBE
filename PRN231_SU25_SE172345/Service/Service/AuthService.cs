using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.IRepo;
using Repository.Model;
using Service.Iservice;

namespace Service.Service
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _repo;
        public AuthService(IUserRepository repo) => _repo = repo;

        public User ValidateUser(string email, string password)
        {
            return _repo.GetByEmailPassword(email, password);
        }
    }

}

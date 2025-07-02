using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Model;

namespace Repository.IRepo
{
    public interface IUserRepository
    {
        User GetByEmailPassword(string email, string password);
    }

}

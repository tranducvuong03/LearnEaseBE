using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Model;

namespace Service.Iservice
{
    public interface IAuthService
    {
        User ValidateUser(string email, string password);
    }

}

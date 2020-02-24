using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SynerzipInterviewApp.Models.Repository
{
    public interface IAuthenticationRepository
    {
        User Login(Login login);
        List<User> GetUsers();
    }
}

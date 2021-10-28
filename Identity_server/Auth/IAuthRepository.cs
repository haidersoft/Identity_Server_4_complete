
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using identity_server_4.Auth.Model;

namespace identity_server_4.Auth
{
    public interface IAuthRepository
    {
        IdentityUser GetUserById(string id);
        IdentityUser GetUserByUsername(string username);
        bool ValidatePassword(string username, string plainTextPassword);
    }
}

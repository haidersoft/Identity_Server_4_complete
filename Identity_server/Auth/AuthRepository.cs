using identity_server_4.Auth.Model;
using identity_server_4.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace identity_server_4.Auth
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthRepository(ApplicationDbContext applicationDbContext, SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager)
        {
            _applicationDbContext = applicationDbContext;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IdentityUser GetUserById(string id)
        {
            return _applicationDbContext.Users.Where(u => u.Id == id).FirstOrDefault();
          
        }

        public IdentityUser GetUserByUsername(string username)
        {
           return _applicationDbContext.Users.Where(u => String.Equals(u.Email, username)).FirstOrDefault();
           
        }

        public bool ValidatePassword(string username, string plainTextPassword)
        {
            return true;

            //var user = GetUserByUsername(username);
            ////var result = await _signInManager.PasswordSignInAsync(username, plainTextPassword, false, false);

            //var result = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, plainTextPassword);



            //if (result == PasswordVerificationResult.Success)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
        }
    }
}

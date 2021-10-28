using IdentityServer4.Models;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace identity_server_4.Auth
{
    public class UserValidator : IResourceOwnerPasswordValidator
    {
        private readonly Dictionary<string, string> users;

        public UserValidator()
        {
            // a static set of username, passwords
            // for demonstration
            // in production scenarios, we can inject a
            // repository or DbContext via DependencyInjection
            // into the constructor
            this.users = new Dictionary<string, string>() {
            { "admin", "Abcd@1234" }
        };
        }

        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var username = context.UserName;
            var password = context.Password;

            if (this.users.Any(x => x.Key == username && x.Value == password))
            {
                // context set to success
                context.Result = new GrantValidationResult(
                    subject: username,
                    authenticationMethod: "custom",
                    claims: new Claim[] {
                    new Claim(ClaimTypes.NameIdentifier, username)
                    }
                );

                return Task.FromResult(0);
            }

            // context set to Failure        
            context.Result = new GrantValidationResult(
                    TokenRequestErrors.UnauthorizedClient, "Invalid Crdentials");

            return Task.FromResult(0);
        }
    }
}

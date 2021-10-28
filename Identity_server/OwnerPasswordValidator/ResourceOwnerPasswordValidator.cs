using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using identity_server_4.Auth;
using IdentityServer4.Models;
using IdentityServer4.Validation;

namespace identity_server_4.OwnerPasswordValidator
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IAuthRepository _authRepository;

        public ResourceOwnerPasswordValidator(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public  Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
          //var result =    _authRepository.ValidatePassword(context.UserName, context.Password);
            if (_authRepository.ValidatePassword(context.UserName, context.Password))
            {
                context.Result = new GrantValidationResult( _authRepository.GetUserByUsername(context.UserName).Id, "password", null, "local", null);
                return  Task.FromResult(context.Result);
            }
            context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "The username and password do not match", null);
            return Task.FromResult(context.Result);

            throw new NotImplementedException();
        }
    }
}

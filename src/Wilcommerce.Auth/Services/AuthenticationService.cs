using Microsoft.AspNetCore.Http.Authentication;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Wilcommerce.Core.Common.Domain.Models;
using Wilcommerce.Core.Common.Domain.ReadModels;
using Microsoft.AspNetCore.Identity;
using Wilcommerce.Auth.Services.Interfaces;
using Wilcommerce.Auth.Models;
using Wilcommerce.Auth.Repository;
using Wilcommerce.Auth.Commands.Handlers.Interfaces;
using Wilcommerce.Auth.Commands;

namespace Wilcommerce.Auth.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public AuthenticationManager AuthenticationManager { get; }

        public ICommonDatabase CommonDatabase { get; }

        public IPasswordHasher<User> PasswordHasher { get; }

        public ITokenGenerator TokenGenerator { get; }

        public IRecoverPasswordCommandHandler RecoverPasswordHandler { get; }

        public AuthenticationService(AuthenticationManager authenticationManager, ICommonDatabase commonDatabase, IPasswordHasher<User> passwordHasher, ITokenGenerator tokenGenerator, IRecoverPasswordCommandHandler recoverPasswordHandler)
        {
            AuthenticationManager = authenticationManager;
            CommonDatabase = commonDatabase;
            PasswordHasher = passwordHasher;
            TokenGenerator = tokenGenerator;
            RecoverPasswordHandler = recoverPasswordHandler;
        }

        public Task SignIn(string email, string password)
        {
            try
            {
                var user = CommonDatabase.Users
                    .Where(u => u.IsActive && u.DisabledOn == null)
                    .FirstOrDefault(u => u.Email == email);

                if (user == null)
                {
                    throw new InvalidOperationException($"User {email} not found");
                }

                if (!IsPasswordValid(user, password))
                {
                    throw new InvalidOperationException("Bad credentials");
                }

                var principal = CreatePrincipalForUser(user);
                return AuthenticationManager.SignInAsync(AuthenticationDefaults.AuthenticationScheme, principal);
            }
            catch 
            {
                throw;
            }
        }

        public Task SignOut()
        {
            try
            {
                return AuthenticationManager.SignOutAsync(AuthenticationDefaults.AuthenticationScheme);
            }
            catch 
            {
                throw;
            }
        }

        public Task RecoverPassword(string email)
        {
            try
            {
                var user = CommonDatabase.Users
                    .Where(u => u.IsActive && u.DisabledOn == null)
                    .FirstOrDefault(u => u.Email == email);

                if (user == null)
                {
                    throw new InvalidOperationException($"User {email} not found");
                }

                var command = new RecoverPasswordCommand(user, TokenGenerator.GenerateForUser(user));
                return RecoverPasswordHandler.Handle(command);
            }
            catch 
            {
                throw;
            }
        }

        #region Protected methods
        protected virtual bool IsPasswordValid(User user, string password)
        {
            try
            {
                var result = PasswordHasher.VerifyHashedPassword(user, user.Password, password);
                return result != PasswordVerificationResult.Failed;
            }
            catch 
            {
                throw;
            }
        }

        protected virtual ClaimsPrincipal CreatePrincipalForUser(User user)
        {
            try
            {
                var identity = new ClaimsIdentity();
                identity.AddClaim(new Claim(ClaimTypes.Name, user.Email));
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
                identity.AddClaim(new Claim(ClaimTypes.Role, GetRoleStringForUser(user)));
                identity.AddClaim(new Claim(ClaimTypes.GivenName, user.Name));

                return new ClaimsPrincipal(identity);
            }
            catch 
            {
                throw;
            }
        }

        protected virtual string GetRoleStringForUser(User user)
        {
            var role = user.Role;
            switch (role)
            {
                case User.Roles.CUSTOMER:
                    return AuthenticationDefaults.CustomerRole;
                case User.Roles.ADMINISTRATOR:
                    return AuthenticationDefaults.AdministratorRole;
                default:
                    return null;
            }
        }
        #endregion
    }
}

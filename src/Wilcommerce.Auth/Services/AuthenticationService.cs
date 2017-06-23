using Microsoft.AspNetCore.Http.Authentication;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Wilcommerce.Core.Common.Domain.Models;
using Wilcommerce.Core.Common.Domain.ReadModels;

namespace Wilcommerce.Auth.Services
{
    public class AuthenticationService : Interfaces.IAuthenticationService
    {
        public AuthenticationManager AuthenticationManager { get; }

        public ICommonDatabase CommonDatabase { get; }

        public AuthenticationService(AuthenticationManager authenticationManager, ICommonDatabase commonDatabase)
        {
            AuthenticationManager = authenticationManager;
            CommonDatabase = commonDatabase;
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
            throw new NotImplementedException();
        }

        #region Protected methods
        protected virtual ClaimsPrincipal CreatePrincipalForUser(User user)
        {
            try
            {
                var identity = new ClaimsIdentity();
                identity.AddClaim(new Claim(ClaimTypes.Name, user.Email));
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
                identity.AddClaim(new Claim(ClaimTypes.Role, GetRoleStringForUser(user)));

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

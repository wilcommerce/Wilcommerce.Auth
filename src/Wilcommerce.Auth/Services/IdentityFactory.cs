using System.Security.Claims;
using Wilcommerce.Core.Common.Domain.Models;

namespace Wilcommerce.Auth.Services
{
    public class IdentityFactory : Interfaces.IIdentityFactory
    {
        public ClaimsPrincipal CreateIdentity(User user)
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

        /// <summary>
        /// Get the string representing the user's role
        /// </summary>
        /// <param name="user">The user instance</param>
        /// <returns>The user's role as a string</returns>
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
    }
}

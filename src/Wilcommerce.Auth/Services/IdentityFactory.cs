using System;
using System.Security.Claims;
using Wilcommerce.Auth.Services.Interfaces;
using Wilcommerce.Core.Common.Domain.Models;

namespace Wilcommerce.Auth.Services
{
    /// <summary>
    /// Implementation of <see cref="IIdentityFactory"/>
    /// </summary>
    public class IdentityFactory : IIdentityFactory
    {
        /// <see cref="IIdentityFactory.CreateIdentity(User)" />
        public virtual ClaimsPrincipal CreateIdentity(User user)
        {
            try
            {
                if (user == null)
                {
                    throw new ArgumentNullException(nameof(user));
                }

                var identity = new ClaimsIdentity();
                identity.AddClaim(new Claim(ClaimTypes.Name, user.Email));
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
                identity.AddClaim(new Claim(ClaimTypes.Role, GetUserRoleAsString(user)));
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
        protected virtual string GetUserRoleAsString(User user)
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

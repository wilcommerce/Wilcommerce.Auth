using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Claims;
using Wilcommerce.Auth.Models;

namespace Wilcommerce.Auth
{
    /// <summary>
    /// Defines the extension methods for the UserManager class
    /// </summary>
    public static class UserManagerExtensions
    {
        /// <summary>
        /// Get the user's full name or the user's name if not exists
        /// </summary>
        /// <param name="userManager">The UserManager instance</param>
        /// <param name="principal">The claims principal instance</param>
        /// <returns>The user's full name</returns>
        public static string GetUserFullName(this UserManager<User> userManager, ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }

            return principal.FindFirstValue(ClaimTypes.GivenName) ?? userManager.GetUserName(principal);
        }
    }
}

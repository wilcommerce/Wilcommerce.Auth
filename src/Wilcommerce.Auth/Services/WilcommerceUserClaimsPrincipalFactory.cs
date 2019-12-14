using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Threading.Tasks;
using Wilcommerce.Auth.Models;

namespace Wilcommerce.Auth.Services
{
    /// <summary>
    /// Overrides <see cref="UserClaimsPrincipalFactory{TUser, TRole}"/> to add custom claims
    /// </summary>
    public class WilcommerceUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<User, IdentityRole>
    {
        /// <summary>
        /// Construct the WilcommerceUserClaimsPrincipalFactory
        /// </summary>
        /// <param name="userManager">The user manager instance</param>
        /// <param name="roleManager">The role manager instance</param>
        /// <param name="options">The identity options</param>
        public WilcommerceUserClaimsPrincipalFactory(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IOptions<IdentityOptions> options) : 
            base(userManager, roleManager, options)
        {
        }

        /// <summary>
        /// Overrides <see cref="UserClaimsPrincipalFactory{TUser, TRole}.GenerateClaimsAsync(TUser)"/> to add custom claims
        /// </summary>
        /// <param name="user">The user to create a System.Security.Claims.ClaimsIdentity from.</param>
        /// <returns>The System.Threading.Tasks.Task that represents the asynchronous creation operation, containing the created System.Security.Claims.ClaimsIdentity.</returns>
        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
            identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
            identity.AddClaim(new Claim(ClaimTypes.GivenName, user.Name));

            return identity;
        }
    }
}

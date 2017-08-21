using System.Security.Claims;
using Wilcommerce.Core.Common.Domain.Models;

namespace Wilcommerce.Auth.Services.Interfaces
{
    /// <summary>
    /// Represents the factory to create the indentity
    /// </summary>
    public interface IIdentityFactory
    {
        /// <summary>
        /// Create the claims principal by the user
        /// </summary>
        /// <param name="user">The user for which creates the indentity</param>
        /// <returns>The claims principal</returns>
        ClaimsPrincipal CreateIdentity(User user);
    }
}

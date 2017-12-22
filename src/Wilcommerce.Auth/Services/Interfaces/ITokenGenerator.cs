using Wilcommerce.Core.Common.Domain.Models;

namespace Wilcommerce.Auth.Services.Interfaces
{
    /// <summary>
    /// Represents the token generator interface
    /// </summary>
    public interface ITokenGenerator
    {
        /// <summary>
        /// Generate a token string for the specified user
        /// </summary>
        /// <param name="user">The current user</param>
        /// <returns>The token string</returns>
        string GenerateForUser(User user);
    }
}

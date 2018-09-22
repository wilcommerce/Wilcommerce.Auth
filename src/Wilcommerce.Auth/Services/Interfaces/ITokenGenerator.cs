using System.Threading.Tasks;
using Wilcommerce.Auth.Models;

namespace Wilcommerce.Auth.Services.Interfaces
{
    /// <summary>
    /// Represents the token generator interface
    /// </summary>
    public interface ITokenGenerator
    {
        /// <summary>
        /// Generate an email confirmation token string for the specified user
        /// </summary>
        /// <param name="user">The current user</param>
        /// <returns>The token string</returns>
        Task<string> GenerateEmailConfirmationTokenForUser(User user);

        /// <summary>
        /// Generate a password recovery token string for the specified user
        /// </summary>
        /// <param name="user">The current user</param>
        /// <returns>The token string</returns>
        Task<string> GeneratePasswordRecoveryTokenForUser(User user);
    }
}

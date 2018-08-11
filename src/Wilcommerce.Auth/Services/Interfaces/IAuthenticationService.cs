using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Wilcommerce.Auth.Services.Interfaces
{
    /// <summary>
    /// Defines all the authentication actions
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Sign in the user using the specified credentials
        /// </summary>
        /// <param name="username">The username</param>
        /// <param name="password">The password</param>
        /// <param name="isPersistent">Whether the authentication is persistent</param>
        /// <returns></returns>
        Task<SignInResult> SignIn(string username, string password, bool isPersistent);

        /// <summary>
        /// Sign out the authenticated user
        /// </summary>
        /// <returns></returns>
        Task SignOut();
    }
}

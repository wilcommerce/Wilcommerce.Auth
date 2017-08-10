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
        Task SignIn(string username, string password, bool isPersistent);

        /// <summary>
        /// Sign out the authenticated user
        /// </summary>
        /// <returns></returns>
        Task SignOut();

        /// <summary>
        /// Perform the password recovery request
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task RecoverPassword(string email);

        /// <summary>
        /// Validate the password recovery request by the specified token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task ValidatePasswordRecovery(string token);
    }
}

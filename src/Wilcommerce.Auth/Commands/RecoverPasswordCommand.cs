using Wilcommerce.Core.Common.Domain.Models;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Auth.Commands
{
    /// <summary>
    /// Recover the user password
    /// </summary>
    public class RecoverPasswordCommand : ICommand
    {
        /// <summary>
        /// Get the user information
        /// </summary>
        public User UserInfo { get; }

        /// <summary>
        /// Get the recovery token
        /// </summary>
        public string Token { get; }

        /// <summary>
        /// Construct the command
        /// </summary>
        /// <param name="user">The user information</param>
        /// <param name="token">The recovery token</param>
        public RecoverPasswordCommand(User user, string token)
        {
            UserInfo = user;
            Token = token;
        }
    }
}

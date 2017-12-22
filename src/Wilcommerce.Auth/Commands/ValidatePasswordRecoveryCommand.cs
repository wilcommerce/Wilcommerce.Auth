using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Auth.Commands
{
    /// <summary>
    /// Validate the password recovery request
    /// </summary>
    public class ValidatePasswordRecoveryCommand : ICommand
    {
        /// <summary>
        /// Get the recovery token
        /// </summary>
        public string Token { get; }

        /// <summary>
        /// Construct the command
        /// </summary>
        /// <param name="token">The recovery token</param>
        public ValidatePasswordRecoveryCommand(string token)
        {
            Token = token;
        }
    }
}

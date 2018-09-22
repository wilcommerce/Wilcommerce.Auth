using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Auth.Commands.User
{
    /// <summary>
    /// Reset the user password
    /// </summary>
    public class ResetPasswordCommand : ICommand
    {
        /// <summary>
        /// Get the user's id
        /// </summary>
        public Guid UserId { get; }

        /// <summary>
        /// Get the reset token for the user
        /// </summary>
        public string ResetToken { get; }

        /// <summary>
        /// Get the new user's password
        /// </summary>
        public string NewPassword { get; }

        /// <summary>
        /// Construct the reset password command
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <param name="resetToken">The reset token</param>
        /// <param name="newPassword">The new password</param>
        public ResetPasswordCommand(Guid userId, string resetToken, string newPassword)
        {
            UserId = userId;
            ResetToken = resetToken;
            NewPassword = newPassword;
        }
    }
}

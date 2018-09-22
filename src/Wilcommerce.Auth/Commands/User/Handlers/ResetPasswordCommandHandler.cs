using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Auth.Commands.User.Handlers
{
    /// <summary>
    /// Implementation of <see cref="Interfaces.IResetPasswordCommandHandler"/>
    /// </summary>
    public class ResetPasswordCommandHandler : Interfaces.IResetPasswordCommandHandler
    {
        /// <summary>
        /// Get the user manager instance
        /// </summary>
        public UserManager<Models.User> UserManager { get; }

        /// <summary>
        /// Get the event bus
        /// </summary>
        public IEventBus EventBus { get; }

        /// <summary>
        /// Construct the command handler
        /// </summary>
        /// <param name="userManager">The user manager</param>
        /// <param name="eventBus">The event bus</param>
        public ResetPasswordCommandHandler(UserManager<Models.User> userManager, IEventBus eventBus)
        {
            UserManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            EventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        /// <summary>
        /// Reset the user's password
        /// </summary>
        /// <param name="command">The command to execute</param>
        /// <returns></returns>
        public async Task Handle(ResetPasswordCommand command)
        {
            try
            {
                var user = await UserManager.FindByIdAsync(command.UserId.ToString());
                var result = await UserManager.ResetPasswordAsync(user, command.ResetToken, command.NewPassword);

                if (!result.Succeeded)
                {
                    throw new ApplicationException("There was an error resetting the user password");
                }
            }
            catch 
            {
                throw;
            }
        }
    }
}

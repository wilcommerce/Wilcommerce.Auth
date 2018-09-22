using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using Wilcommerce.Auth.Events.User;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Auth.Commands.User.Handlers
{
    /// <summary>
    /// Implementation of <see cref="Interfaces.IChangeUserInfoCommandHandler"/>
    /// </summary>
    public class ChangeUserInfoCommandHandler : Interfaces.IChangeUserInfoCommandHandler
    {
        /// <summary>
        /// Get the user manager instance
        /// </summary>
        public UserManager<Models.User> UserManager { get; }

        /// <summary>
        /// Get the event bus instance
        /// </summary>
        public IEventBus EventBus { get; }

        /// <summary>
        /// Construct the command handler
        /// </summary>
        /// <param name="userManager">The user manager</param>
        /// <param name="eventBus">The event bus</param>
        public ChangeUserInfoCommandHandler(UserManager<Models.User> userManager, IEventBus eventBus)
        {
            UserManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            EventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        /// <summary>
        /// Change the user's information
        /// </summary>
        /// <param name="command">The command to execute</param>
        /// <returns></returns>
        public async Task Handle(ChangeUserInfoCommand command)
        {
            try
            {
                var user = await UserManager.FindByIdAsync(command.UserId.ToString());
                if (command.Name != user.Name)
                {
                    user.ChangeName(command.Name);
                }

                if (command.ProfileImage != user.ProfileImage)
                {
                    user.SetProfileImage(command.ProfileImage);
                }

                var result = await UserManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    throw new ApplicationException("Error while changing the user's info");
                }

                var @event = new UserInfoChangedEvent(command.UserId, command.Name, command.ProfileImage);
                EventBus.RaiseEvent(@event);
            }
            catch 
            {
                throw;
            }
        }
    }
}

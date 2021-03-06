﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using Wilcommerce.Auth.Events.User;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Auth.Commands.User.Handlers
{
    /// <summary>
    /// Implementation of <see cref="Interfaces.IDisableUserCommandHandler"/>
    /// </summary>
    public class DisableUserCommandHandler : Interfaces.IDisableUserCommandHandler
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
        public DisableUserCommandHandler(UserManager<Models.User> userManager, IEventBus eventBus)
        {
            UserManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            EventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        /// <summary>
        /// Disable the user
        /// </summary>
        /// <param name="command">The command to execute</param>
        /// <returns></returns>
        public async Task Handle(DisableUserCommand command)
        {
            try
            {
                var user = await UserManager.FindByIdAsync(command.UserId.ToString());
                user.Disable();

                var result = await UserManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    throw new ApplicationException("Error while disabling the user");
                }

                var @event = new UserDisabledEvent(Guid.Parse(user.Id));
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }
    }
}

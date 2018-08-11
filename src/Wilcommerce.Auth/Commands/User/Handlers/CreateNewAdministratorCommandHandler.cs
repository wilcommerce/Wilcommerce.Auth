using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using Wilcommerce.Auth.Events.User;

namespace Wilcommerce.Auth.Commands.User.Handlers
{
    /// <summary>
    /// Implementation of <see cref="Interfaces.ICreateNewAdministratorCommandHandler"/>
    /// </summary>
    public class CreateNewAdministratorCommandHandler : Interfaces.ICreateNewAdministratorCommandHandler
    {
        /// <summary>
        /// Get the user mananager instance
        /// </summary>
        public UserManager<Models.User> UserManager { get; }

        /// <summary>
        /// The event bus instance
        /// </summary>
        public Core.Infrastructure.IEventBus EventBus { get; }

        /// <summary>
        /// Construct the command handler
        /// </summary>
        /// <param name="userManager">The user manager</param>
        /// <param name="eventBus">The event bus</param>
        public CreateNewAdministratorCommandHandler(UserManager<Models.User> userManager, Core.Infrastructure.IEventBus eventBus)
        {
            UserManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            EventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        /// <summary>
        /// Create a new administrator
        /// </summary>
        /// <param name="command">The command to execute</param>
        /// <returns></returns>
        public async Task Handle(CreateNewAdministratorCommand command)
        {
            try
            {
                var administrator = Models.User.CreateAsAdministrator(command.Name, command.Email, command.IsActive);
                var result = await UserManager.CreateAsync(administrator, command.Password);
                if (!result.Succeeded)
                {
                    throw new InvalidOperationException(string.Join(",", result.Errors));
                }

                await UserManager.AddToRoleAsync(administrator, AuthenticationDefaults.AdministratorRole);

                var @event = new NewAdministratorCreatedEvent(administrator.Id, administrator.Name, administrator.Email);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }
    }
}

using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using Wilcommerce.Auth.Events.User;
using Wilcommerce.Auth.Services.Interfaces;

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
        /// Get the role factory instance
        /// </summary>
        public IRoleFactory RoleFactory { get; }

        /// <summary>
        /// Construct the command handler
        /// </summary>
        /// <param name="userManager">The user manager</param>
        /// <param name="eventBus">The event bus</param>
        /// <param name="roleFactory">The role factory</param>
        public CreateNewAdministratorCommandHandler(UserManager<Models.User> userManager, Core.Infrastructure.IEventBus eventBus, IRoleFactory roleFactory)
        {
            UserManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            EventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            RoleFactory = roleFactory ?? throw new ArgumentNullException(nameof(roleFactory));
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

                var role = await RoleFactory.Administrator();
                await UserManager.AddToRoleAsync(administrator, role.Name);

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

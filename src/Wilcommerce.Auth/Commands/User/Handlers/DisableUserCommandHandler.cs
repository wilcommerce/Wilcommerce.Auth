using System.Threading.Tasks;
using Wilcommerce.Auth.Events.User;
using Wilcommerce.Auth.Repository;

namespace Wilcommerce.Auth.Commands.User.Handlers
{
    /// <summary>
    /// Implementation of <see cref="Interfaces.IDisableUserCommandHandler"/>
    /// </summary>
    public class DisableUserCommandHandler : Interfaces.IDisableUserCommandHandler
    {
        /// <summary>
        /// Get the  repository instance
        /// </summary>
        public IRepository Repository { get; }

        /// <summary>
        /// Get the event bus instance
        /// </summary>
        public Core.Infrastructure.IEventBus EventBus { get; }

        /// <summary>
        /// Construct the command handler
        /// </summary>
        /// <param name="repository">The repository</param>
        /// <param name="eventBus">The event bus</param>
        public DisableUserCommandHandler(IRepository repository, Core.Infrastructure.IEventBus eventBus)
        {
            Repository = repository;
            EventBus = eventBus;
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
                var user = await Repository.GetByKeyAsync<Models.User>(command.UserId);
                user.Disable();

                await Repository.SaveChangesAsync();

                var @event = new UserDisabledEvent(user.Id);
                EventBus.RaiseEvent(@event);
            }
            catch
            {
                throw;
            }
        }
    }
}

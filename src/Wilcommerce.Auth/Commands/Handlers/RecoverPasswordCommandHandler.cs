using System;
using System.Threading.Tasks;
using Wilcommerce.Auth.Events.User;
using Wilcommerce.Auth.Models;
using Wilcommerce.Auth.Repository;

namespace Wilcommerce.Auth.Commands.Handlers
{
    /// <summary>
    /// Implementation of <see cref="Interfaces.IRecoverPasswordCommandHandler"/>
    /// </summary>
    public class RecoverPasswordCommandHandler : Interfaces.IRecoverPasswordCommandHandler
    {
        /// <summary>
        /// Get the event bus
        /// </summary>
        public Core.Infrastructure.IEventBus EventBus { get; }

        /// <summary>
        /// Get the authentication repository
        /// </summary>
        public IRepository Repository { get; }

        /// <summary>
        /// Construct the command handler
        /// </summary>
        /// <param name="repository">The repository instance</param>
        /// <param name="eventBus">The event bus instance</param>
        public RecoverPasswordCommandHandler(IRepository repository, Core.Infrastructure.IEventBus eventBus)
        {
            Repository = repository ?? throw new ArgumentNullException(nameof(repository));
            EventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        /// <summary>
        /// Recover the password for the user
        /// </summary>
        /// <param name="command">The command to execute</param>
        /// <returns></returns>
        public async Task Handle(RecoverPasswordCommand command)
        {
            try
            {
                var userToken = UserToken.PasswordRecovery(command.UserInfo, command.Token, DateTime.Now.AddDays(AuthenticationDefaults.ExpirationDays));

                Repository.Add(userToken);
                await Repository.SaveChangesAsync();

                var @event = new PasswordRecoveryRequestedEvent(userToken.UserId, command.UserInfo.Email, userToken.Id, userToken.Token, userToken.ExpirationDate);
                EventBus.RaiseEvent(@event);
            }
            catch 
            {
                throw;
            }
        }
    }
}

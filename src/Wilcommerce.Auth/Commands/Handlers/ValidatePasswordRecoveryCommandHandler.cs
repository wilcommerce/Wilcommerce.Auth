using System;
using System.Linq;
using System.Threading.Tasks;
using Wilcommerce.Auth.Events.User;
using Wilcommerce.Auth.Models;
using Wilcommerce.Auth.ReadModels;
using Wilcommerce.Auth.Repository;

namespace Wilcommerce.Auth.Commands.Handlers
{
    /// <summary>
    /// Implementation of <see cref="Interfaces.IValidatePasswordRecoveryCommandHandler"/>
    /// </summary>
    public class ValidatePasswordRecoveryCommandHandler : Interfaces.IValidatePasswordRecoveryCommandHandler
    {
        /// <summary>
        /// Get the authentication database
        /// </summary>
        public IAuthDatabase Database { get; }

        /// <summary>
        /// Get the authentication repository
        /// </summary>
        public IRepository Repository { get; }

        /// <summary>
        /// Get the event bus
        /// </summary>
        public Core.Infrastructure.IEventBus EventBus { get; }

        /// <summary>
        /// Construct the command handler
        /// </summary>
        /// <param name="database">The database instance</param>
        /// <param name="repository">The repository instance</param>
        /// <param name="eventBus">The event bus instance</param>
        public ValidatePasswordRecoveryCommandHandler(IAuthDatabase database, IRepository repository, Core.Infrastructure.IEventBus eventBus)
        {
            Database = database;
            Repository = repository;
            EventBus = eventBus;
        }

        /// <summary>
        /// Validate the password recovery request
        /// </summary>
        /// <param name="command">The command to execute</param>
        /// <returns></returns>
        public async Task Handle(ValidatePasswordRecoveryCommand command)
        {
            try
            {
                var userToken = Database.Tokens
                    .NotExpired()
                    .ByTokenType(TokenTypes.PasswordRecovery)
                    .FirstOrDefault(t => t.Token == command.Token);

                if (userToken == null)
                {
                    throw new InvalidOperationException("Invalid token");
                }

                var token = await Repository.GetByKeyAsync<UserToken>(userToken.Id);
                token.SetAsExpired();

                await Repository.SaveChangesAsync();

                var @event = new PasswordRecoveryValidatedEvent(userToken.UserId, userToken.Token);
                EventBus.RaiseEvent(@event);
            }
            catch 
            {
                throw;
            }
        }
    }
}

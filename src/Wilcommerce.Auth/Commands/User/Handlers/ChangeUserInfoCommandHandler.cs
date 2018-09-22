using System.Threading.Tasks;
using Wilcommerce.Auth.Repository;

namespace Wilcommerce.Auth.Commands.User.Handlers
{
    /// <summary>
    /// Implementation of <see cref="Interfaces.IChangeUserInfoCommandHandler"/>
    /// </summary>
    public class ChangeUserInfoCommandHandler : Interfaces.IChangeUserInfoCommandHandler
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
        public ChangeUserInfoCommandHandler(IRepository repository, Core.Infrastructure.IEventBus eventBus)
        {
            Repository = repository;
            EventBus = eventBus;
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
                var user = await Repository.GetByKeyAsync<Models.User>(command.UserId);
                if (command.Name != user.Name)
                {
                    user.ChangeName(command.Name);
                }

                if (command.ProfileImage != user.ProfileImage)
                {
                    user.SetProfileImage(command.ProfileImage);
                }

                await Repository.SaveChangesAsync();
            }
            catch 
            {
                throw;
            }
        }
    }
}

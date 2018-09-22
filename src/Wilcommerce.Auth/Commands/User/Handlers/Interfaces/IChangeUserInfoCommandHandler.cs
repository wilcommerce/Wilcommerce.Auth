using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Auth.Commands.User.Handlers.Interfaces
{
    /// <summary>
    /// Handles the change of user's information
    /// </summary>
    public interface IChangeUserInfoCommandHandler : ICommandHandlerAsync<ChangeUserInfoCommand>
    {
    }
}

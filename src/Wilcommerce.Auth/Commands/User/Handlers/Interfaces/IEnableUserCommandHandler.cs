using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Auth.Commands.User.Handlers.Interfaces
{
    /// <summary>
    /// Handles the enabling of the user
    /// </summary>
    public interface IEnableUserCommandHandler : ICommandHandlerAsync<EnableUserCommand>
    {
    }
}

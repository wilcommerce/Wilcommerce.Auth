using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Auth.Commands.User.Handlers.Interfaces
{
    /// <summary>
    /// Handles the reset of user's password
    /// </summary>
    public interface IResetPasswordCommandHandler : ICommandHandlerAsync<ResetPasswordCommand>
    {
    }
}

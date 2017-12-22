using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Auth.Commands.Handlers.Interfaces
{
    /// <summary>
    /// Performs the password recovery action
    /// </summary>
    public interface IRecoverPasswordCommandHandler : ICommandHandlerAsync<RecoverPasswordCommand>
    {
    }
}

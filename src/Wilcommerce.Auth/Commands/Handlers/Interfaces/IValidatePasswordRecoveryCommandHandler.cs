using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Auth.Commands.Handlers.Interfaces
{
    /// <summary>
    /// Performs the validation for the password recovery request
    /// </summary>
    public interface IValidatePasswordRecoveryCommandHandler : ICommandHandlerAsync<ValidatePasswordRecoveryCommand>
    {
    }
}

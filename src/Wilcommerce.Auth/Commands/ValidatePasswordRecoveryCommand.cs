using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Auth.Commands
{
    public class ValidatePasswordRecoveryCommand : ICommand
    {
        public string Token { get; }

        public ValidatePasswordRecoveryCommand(string token)
        {
            Token = token;
        }
    }
}

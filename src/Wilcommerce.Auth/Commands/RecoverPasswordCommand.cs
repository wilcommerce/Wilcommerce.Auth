using Wilcommerce.Core.Common.Domain.Models;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Auth.Commands
{
    public class RecoverPasswordCommand : ICommand
    {
        public User UserInfo { get; }

        public string Token { get; }

        public RecoverPasswordCommand(User user, string token)
        {
            UserInfo = user;
            Token = token;
        }
    }
}

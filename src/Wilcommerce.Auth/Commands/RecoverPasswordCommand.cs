using Wilcommerce.Core.Common.Domain.Models;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Auth.Commands
{
    public class RecoverPasswordCommand : ICommand
    {
        public User UserInfo { get; set; }
    }
}

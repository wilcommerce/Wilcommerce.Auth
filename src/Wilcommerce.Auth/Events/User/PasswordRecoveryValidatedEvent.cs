using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Auth.Events.User
{
    public class PasswordRecoveryValidatedEvent : DomainEvent
    {
        public string Token { get; }

        public PasswordRecoveryValidatedEvent(Guid userId, string token)
            : base(userId, typeof(Core.Common.Domain.Models.User))
        {
            Token = token;
        }
    }
}

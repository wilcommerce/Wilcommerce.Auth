using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Auth.Events.User
{
    public class UserSignedInEvent : DomainEvent
    {
        public string Username { get; }

        public UserSignedInEvent(Guid userId, string username)
            : base(userId, typeof(Core.Common.Domain.Models.User))
        {
            Username = username;
        }
    }
}

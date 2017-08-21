using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Auth.Events.User
{
    public class PasswordRecoveryRequestedEvent : DomainEvent
    {
        public string Username { get; }

        public Guid TokenId { get; }

        public string Token { get; }

        public DateTime ExpirationDate { get; }

        public PasswordRecoveryRequestedEvent(Guid userId, string username, Guid tokenId, string token, DateTime expirationDate)
            : base(userId, typeof(Core.Common.Domain.Models.User))
        {
            Username = username;
            TokenId = tokenId;
            Token = token;
            ExpirationDate = expirationDate;
        }
    }
}

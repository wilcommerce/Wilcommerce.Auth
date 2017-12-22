using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Auth.Events.User
{
    /// <summary>
    /// Password recovery validated
    /// </summary>
    public class PasswordRecoveryRequestedEvent : DomainEvent
    {
        /// <summary>
        /// Get the username
        /// </summary>
        public string Username { get; }

        /// <summary>
        /// Get the token id
        /// </summary>
        public Guid TokenId { get; }

        /// <summary>
        /// Get the password recovery token
        /// </summary>
        public string Token { get; }

        /// <summary>
        /// Get the token expiration date
        /// </summary>
        public DateTime ExpirationDate { get; }

        /// <summary>
        /// Construct the event
        /// </summary>
        /// <param name="userId">The user id</param>
        /// <param name="username">The username</param>
        /// <param name="tokenId">The token id</param>
        /// <param name="token">The password recovery token</param>
        /// <param name="expirationDate">The token expiration date</param>
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

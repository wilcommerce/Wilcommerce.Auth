using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Auth.Events.User
{
    /// <summary>
    /// Password recovery validated
    /// </summary>
    public class PasswordRecoveryValidatedEvent : DomainEvent
    {
        /// <summary>
        /// Get the password recovery token
        /// </summary>
        public string Token { get; }

        /// <summary>
        /// Construct the event
        /// </summary>
        /// <param name="userId">The user id</param>
        /// <param name="token">The password recovery token</param>
        public PasswordRecoveryValidatedEvent(Guid userId, string token)
            : base(userId, typeof(Core.Common.Domain.Models.User))
        {
            Token = token;
        }
    }
}

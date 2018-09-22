using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Auth.Events.User
{
    /// <summary>
    /// User password reset
    /// </summary>
    public class UserPasswordResetEvent : DomainEvent
    {
        /// <summary>
        /// Get the user id
        /// </summary>
        public Guid UserId { get; private set; }

        /// <summary>
        /// Construct the event
        /// </summary>
        /// <param name="userId"></param>
        public UserPasswordResetEvent(Guid userId)
            : base(userId, typeof(Models.User))
        {
            UserId = userId;
        }
    }
}

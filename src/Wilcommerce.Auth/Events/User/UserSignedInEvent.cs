using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Auth.Events.User
{
    /// <summary>
    /// User signed in
    /// </summary>
    public class UserSignedInEvent : DomainEvent
    {
        /// <summary>
        /// Get the username
        /// </summary>
        public string Username { get; }

        /// <summary>
        /// Construct the event
        /// </summary>
        /// <param name="userId">The user id</param>
        /// <param name="username">The username</param>
        public UserSignedInEvent(Guid userId, string username)
            : base(userId, typeof(Core.Common.Domain.Models.User))
        {
            Username = username;
        }
    }
}

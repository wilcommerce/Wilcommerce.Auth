﻿using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Auth.Events.User
{
    /// <summary>
    /// User disabled
    /// </summary>
    public class UserDisabledEvent : DomainEvent
    {
        /// <summary>
        /// Get the user id
        /// </summary>
        public Guid UserId { get; private set; }

        /// <summary>
        /// Construct the event
        /// </summary>
        /// <param name="userId">The user id</param>
        public UserDisabledEvent(Guid userId)
            : base(userId, typeof(Models.User))
        {
            UserId = userId;
        }

        /// <summary>
        /// Convert the event to string
        /// </summary>
        /// <returns>The converted string</returns>
        public override string ToString()
        {
            return $"User {UserId} disabled";
        }
    }
}

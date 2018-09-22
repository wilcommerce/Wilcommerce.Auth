using System;
using Wilcommerce.Core.Common.Models;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Auth.Events.User
{
    /// <summary>
    /// User information changed
    /// </summary>
    public class UserInfoChangedEvent : DomainEvent
    {
        /// <summary>
        /// Get the user id
        /// </summary>
        public Guid UserId { get; private set; }

        /// <summary>
        /// Get the user name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Get the user profile image
        /// </summary>
        public Image ProfileImage { get; private set; }

        /// <summary>
        /// Construct the event
        /// </summary>
        /// <param name="userId">The user id</param>
        /// <param name="name">The new user name</param>
        /// <param name="profileImage">The new user profile image</param>
        public UserInfoChangedEvent(Guid userId, string name, Image profileImage)
            : base(userId, typeof(Models.User))
        {
            UserId = userId;
            Name = name;
            ProfileImage = profileImage;
        }
    }
}

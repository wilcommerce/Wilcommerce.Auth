using System;
using Wilcommerce.Core.Common.Models;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Auth.Commands.User
{
    /// <summary>
    /// Update the user information
    /// </summary>
    public class ChangeUserInfoCommand : ICommand
    {
        /// <summary>
        /// Get the user's id
        /// </summary>
        public Guid UserId { get; }

        /// <summary>
        /// Get the user's name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Get the user's profile image
        /// </summary>
        public Image ProfileImage { get; }

        /// <summary>
        /// Construct the change user info command
        /// </summary>
        /// <param name="userId">The user id</param>
        /// <param name="name">The user name</param>
        /// <param name="profileImage">The user profile image</param>
        public ChangeUserInfoCommand(Guid userId, string name, Image profileImage)
        {
            UserId = userId;
            Name = name;
            ProfileImage = profileImage;
        }
    }
}

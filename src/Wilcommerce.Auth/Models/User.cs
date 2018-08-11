using Microsoft.AspNetCore.Identity;
using System;
using Wilcommerce.Core.Common.Domain.Models;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Auth.Models
{
    /// <summary>
    /// Represents the user
    /// </summary>
    public class User : IdentityUser, IAggregateRoot
    {
        /// <summary>
        /// Get the user's id in Guid format
        /// </summary>
        public new Guid Id => Guid.Parse(base.Id);

        #region Constructor
        /// <summary>
        /// Construct the user
        /// </summary>
        protected User()
            : base()
        {
            ProfileImage = new Image();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Get or set the user full name
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Get or set the user profile image
        /// </summary>
        public Image ProfileImage { get; protected set; }

        /// <summary>
        /// Get or set whether the user is active
        /// </summary>
        public bool IsActive { get; protected set; }

        /// <summary>
        /// Get or set the date and time of when the user was disabled
        /// </summary>
        public DateTime? DisabledOn { get; set; }
        #endregion

        #region Behaviors
        /// <summary>
        /// Enable the user
        /// </summary>
        public virtual void Enable()
        {
            IsActive = true;
            if (DisabledOn != null)
            {
                DisabledOn = null;
            }
        }

        /// <summary>
        /// Disable the user
        /// </summary>
        public virtual void Disable()
        {
            IsActive = false;
            DisabledOn = DateTime.Now;
        }

        /// <summary>
        /// Change the user's name
        /// </summary>
        /// <param name="name">The new user's name</param>
        public virtual void ChangeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("value cannot be empty", nameof(name));
            }

            Name = name;
        }

        /// <summary>
        /// Set the user's profile image
        /// </summary>
        /// <param name="profileImage">The profile image to set</param>
        public virtual void SetProfileImage(Image profileImage)
        {
            ProfileImage = profileImage ?? throw new ArgumentNullException(nameof(profileImage));
        }
        #endregion

        #region Factory Methods
        /// <summary>
        /// Creates a new administrator user
        /// </summary>
        /// <param name="name">The user full name</param>
        /// <param name="email">The user username</param>
        /// <param name="active">Whether the user is active</param>
        /// <returns>The created administrator user</returns>
        public static User CreateAsAdministrator(string name, string email, bool active)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("value cannot be empty", nameof(name));
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("value cannot be empty", nameof(email));
            }

            var user = new User
            {
                UserName = email,
                Email = email,
                NormalizedEmail = email.ToUpper(),
                NormalizedUserName = email.ToUpper(),
                Name = name,
                IsActive = active,
                EmailConfirmed = true
            };

            return user;
        }

        /// <summary>
        /// Creates a new customer user
        /// </summary>
        /// <param name="name">The user full name</param>
        /// <param name="email">The user username</param>
        /// <returns>The created customer user</returns>
        public static User CreateAsCustomer(string name, string email)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("value cannot be empty", nameof(name));
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("value cannot be empty", nameof(email));
            }

            var user = new User
            {
                UserName = email,
                Email = email,
                NormalizedEmail = email.ToUpper(),
                NormalizedUserName = email.ToUpper(),
                Name = name
            };

            return user;
        }
        #endregion
    }
}

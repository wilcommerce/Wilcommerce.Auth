using System;
using Wilcommerce.Core.Common.Domain.Models;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Auth.Models
{
    /// <summary>
    /// Represents a token created for a specific user
    /// </summary>
    public class UserToken : IAggregateRoot
    {
        /// <summary>
        /// Get the entity id
        /// </summary>
        public Guid Id { get; protected set; }

        #region Properties
        /// <summary>
        /// Get the related user id
        /// </summary>
        public Guid UserId { get; protected set; }

        /// <summary>
        /// Get the token type
        /// </summary>
        public string TokenType { get; protected set; }

        /// <summary>
        /// Get the token
        /// </summary>
        public string Token { get; protected set; }

        /// <summary>
        /// Get the date and time of token creation
        /// </summary>
        public DateTime CreationDate { get; protected set; }

        /// <summary>
        /// Get the date and time of token expiration
        /// </summary>
        public DateTime ExpirationDate { get; protected set; }

        /// <summary>
        /// Get whether the token is expired
        /// </summary>
        public bool IsExpired { get; protected set; }
        #endregion

        #region Public Methods
        /// <summary>
        /// Set the current token as expired
        /// </summary>
        public virtual void SetAsExpired()
        {
            if (IsExpired)
            {
                throw new InvalidOperationException($"Token already expired on {ExpirationDate.ToString()}");
            }

            IsExpired = true;
            ExpirationDate = DateTime.Now;
        }
        #endregion

        #region Factory
        /// <summary>
        /// Create a new password recovery token
        /// </summary>
        /// <param name="user">The user</param>
        /// <param name="token">The token string</param>
        /// <param name="expirationDate">The token expiration date</param>
        /// <returns>The token created</returns>
        public static UserToken PasswordRecovery(Core.Common.Domain.Models.User user, string token, DateTime expirationDate)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentNullException(nameof(token));
            }

            var now = DateTime.Now;
            if (expirationDate < now)
            {
                throw new ArgumentException("Invalid expiration date", nameof(expirationDate));
            }

            var userToken = new UserToken
            {
                UserId = user.Id,
                TokenType = TokenTypes.PasswordRecovery,
                Token = token,
                CreationDate = now,
                ExpirationDate = expirationDate,
                IsExpired = false
            };

            return userToken;
        }

        /// <summary>
        /// Create a new registration token
        /// </summary>
        /// <param name="user">The user</param>
        /// <param name="token">The token string</param>
        /// <param name="expirationDate">The token expiration date</param>
        /// <returns>The token created</returns>
        public static UserToken Registration(Core.Common.Domain.Models.User user, string token, DateTime expirationDate)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentNullException("token");
            }

            var now = DateTime.Now;
            if (expirationDate < now)
            {
                throw new ArgumentException("Invalid expiration date", "expirationDate");
            }

            var userToken = new UserToken
            {
                UserId = user.Id,
                TokenType = TokenTypes.Registration,
                Token = token,
                CreationDate = now,
                ExpirationDate = expirationDate,
                IsExpired = false
            };

            return userToken;
        }
        #endregion
    }
}

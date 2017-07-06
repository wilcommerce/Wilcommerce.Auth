using System;
using Wilcommerce.Core.Common.Domain.Models;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Auth.Models
{
    public class UserToken : IAggregateRoot
    {
        public Guid Id { get; set; }

        #region Properties
        public virtual User User { get; protected set; }

        public Guid UserId { get; protected set; }

        public string TokenType { get; protected set; }

        public string Token { get; protected set; }

        public DateTime CreationDate { get; protected set; }

        public DateTime ExpirationDate { get; protected set; }

        public bool IsExpired { get; protected set; }
        #endregion

        #region Public Methods
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
        public static UserToken PasswordRecovery(User user, string token, DateTime expirationDate)
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
                TokenType = TokenTypes.PasswordRecovery,
                Token = token,
                CreationDate = now,
                ExpirationDate = expirationDate,
                IsExpired = false
            };

            return userToken;
        }

        public static UserToken Registration(User user, string token, DateTime expirationDate)
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

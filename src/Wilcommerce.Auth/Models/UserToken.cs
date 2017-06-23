using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public static UserToken Create(User user, string tokenType, string token, DateTime expirationDate)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (string.IsNullOrEmpty(tokenType))
            {
                throw new ArgumentNullException("tokenType");
            }

            if (!TokenTypes.IsValidType(tokenType))
            {
                throw new ArgumentException("Invalid value", "tokenType");
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
                TokenType = tokenType,
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

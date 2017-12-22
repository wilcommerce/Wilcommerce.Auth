using System;
using System.Linq;
using Wilcommerce.Auth.Models;

namespace Wilcommerce.Auth.ReadModels
{
    /// <summary>
    /// Defines the extension methods for the read models
    /// </summary>
    public static class UserTokenExtensions
    {
        /// <summary>
        /// Retrieve all the user tokens by the user id
        /// </summary>
        /// <param name="tokens">The instance to which attach this method</param>
        /// <param name="userId">The user id</param>
        /// <returns>A list of user tokens</returns>
        public static IQueryable<UserToken> ByUser(this IQueryable<UserToken> tokens, Guid userId)
        {
            return from t in tokens
                   where t.UserId == userId
                   select t;
        }

        /// <summary>
        /// Retrieve all the expired tokens
        /// </summary>
        /// <param name="tokens">The instance to which attach this method</param>
        /// <returns>A list of user tokens</returns>
        public static IQueryable<UserToken> Expired(this IQueryable<UserToken> tokens)
        {
            return from t in tokens
                   where t.IsExpired || t.ExpirationDate <= DateTime.Now
                   select t;
        }

        /// <summary>
        /// Retrieve all the user tokens which are not expired
        /// </summary>
        /// <param name="tokens">The instance to which attach this method</param>
        /// <returns>A list of user tokens</returns>
        public static IQueryable<UserToken> NotExpired(this IQueryable<UserToken> tokens)
        {
            return from t in tokens
                   where !t.IsExpired && t.ExpirationDate > DateTime.Now
                   select t;
        }

        /// <summary>
        /// Retrieve all the user tokens filtered by the type
        /// </summary>
        /// <param name="tokens">The instance to which attach this method</param>
        /// <param name="type">The token type</param>
        /// <returns>A list of user tokens</returns>
        public static IQueryable<UserToken> ByTokenType(this IQueryable<UserToken> tokens, string type)
        {
            return from t in tokens
                   where t.TokenType == type
                   select t;
        }
    }
}

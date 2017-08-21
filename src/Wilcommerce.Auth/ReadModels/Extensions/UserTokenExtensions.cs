using System;
using System.Linq;
using Wilcommerce.Auth.Models;

namespace Wilcommerce.Auth.ReadModels
{
    public static class UserTokenExtensions
    {
        public static IQueryable<UserToken> ByUser(this IQueryable<UserToken> tokens, Guid userId)
        {
            return from t in tokens
                   where t.UserId == userId
                   select t;
        }

        public static IQueryable<UserToken> Expired(this IQueryable<UserToken> tokens)
        {
            return from t in tokens
                   where t.IsExpired || t.ExpirationDate <= DateTime.Now
                   select t;
        }

        public static IQueryable<UserToken> NotExpired(this IQueryable<UserToken> tokens)
        {
            return from t in tokens
                   where !t.IsExpired && t.ExpirationDate > DateTime.Now
                   select t;
        }

        public static IQueryable<UserToken> ByTokenType(this IQueryable<UserToken> tokens, string type)
        {
            return from t in tokens
                   where t.TokenType == type
                   select t;
        }
    }
}

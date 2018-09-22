using System.Linq;
using Wilcommerce.Auth.Models;

namespace Wilcommerce.Auth.ReadModels
{
    /// <summary>
    /// Defines the extension methods for the user read model
    /// </summary>
    public static class UserExtensions
    {
        /// <summary>
        /// Retrieve all active users
        /// </summary>
        /// <param name="users">The instance to which attach this method</param>
        /// <returns>A list of users</returns>
        public static IQueryable<User> Actives(this IQueryable<User> users)
        {
            return from u in users
                   where u.IsActive && u.DisabledOn == null
                   select u;
        }

        /// <summary>
        /// Retrieve the users with the specified username
        /// </summary>
        /// <param name="users">The instance to which attach this method</param>
        /// <param name="username">The user's username</param>
        /// <returns>A list of users</returns>
        public static IQueryable<User> WithUsername(this IQueryable<User> users, string username)
        {
            return from u in users
                   where u.UserName == username
                   select u;
        }
    }
}

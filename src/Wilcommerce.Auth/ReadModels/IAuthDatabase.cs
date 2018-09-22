using System.Linq;
using Wilcommerce.Auth.Models;

namespace Wilcommerce.Auth.ReadModels
{
    /// <summary>
    /// Represent the interface for the authentication and authorization database
    /// </summary>
    public interface IAuthDatabase
    {
        /// <summary>
        /// Get the users list
        /// </summary>
        IQueryable<User> Users { get; }
    }
}

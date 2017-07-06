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
        /// Get the tokens created by the platform
        /// </summary>
        IQueryable<UserToken> Tokens { get; }
    }
}

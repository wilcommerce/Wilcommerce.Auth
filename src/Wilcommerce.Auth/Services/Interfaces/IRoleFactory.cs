using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Wilcommerce.Auth.Services.Interfaces
{
    /// <summary>
    /// Represents the factory to create the available roles
    /// </summary>
    public interface IRoleFactory
    {
        /// <summary>
        /// Create an administrator role if not exists and returns it
        /// </summary>
        /// <returns>The administrator role</returns>
        Task<IdentityRole> Administrator();

        /// <summary>
        /// Create a customer role if not exists and returns it
        /// </summary>
        /// <returns>The customer role</returns>
        Task<IdentityRole> Customer();
    }
}

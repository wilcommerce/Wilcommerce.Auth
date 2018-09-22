using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Wilcommerce.Auth.Services.Interfaces;

namespace Wilcommerce.Auth.Services
{
    /// <summary>
    /// Implementation of <see cref="IRoleFactory"/>
    /// </summary>
    public class RoleFactory : IRoleFactory
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        /// <summary>
        /// Construct the role factory
        /// </summary>
        /// <param name="roleManager">The role manager instance</param>
        public RoleFactory(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        }

        /// <summary>
        /// Implementation of <see cref="IRoleFactory.Administrator"/>
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IdentityRole> Administrator()
        {
            return await CreateRoleIfNotExists(AuthenticationDefaults.CustomerRole);
        }

        /// <summary>
        /// Implementation of <see cref="IRoleFactory.Customer"/>
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IdentityRole> Customer()
        {
            return await CreateRoleIfNotExists(AuthenticationDefaults.CustomerRole);
        }

        #region Protected Methods
        /// <summary>
        /// Create the role with the specified name if not exists and returns it
        /// </summary>
        /// <param name="roleName">The role name</param>
        /// <returns>The role instance</returns>
        protected virtual async Task<IdentityRole> CreateRoleIfNotExists(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                return new IdentityRole(roleName);
            }

            return role;
        }
        #endregion
    }
}

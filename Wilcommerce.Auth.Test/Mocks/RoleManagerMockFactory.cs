using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;

namespace Wilcommerce.Auth.Test.Mocks
{
    public static class RoleManagerMockFactory
    {
        public static RoleManager<IdentityRole> BuildRoleManager(IRoleStore<IdentityRole> store = null)
        {
            if (store == null)
            {
                store = new Mock<IRoleStore<IdentityRole>>().Object;
            }

            IEnumerable<IRoleValidator<IdentityRole>> roleValidators = null;
            ILookupNormalizer keyNormalizer = null;
            IdentityErrorDescriber errors = null;
            ILogger<RoleManager<IdentityRole>> logger = null;

            var roleManager = new RoleManager<IdentityRole>(store, roleValidators, keyNormalizer, errors, logger);

            return roleManager;
        }
    }
}

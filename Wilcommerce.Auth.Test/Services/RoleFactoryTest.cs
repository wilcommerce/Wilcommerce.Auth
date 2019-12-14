using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Wilcommerce.Auth.Services;
using Xunit;

namespace Wilcommerce.Auth.Test.Services
{
    public class RoleFactoryTest
    {
        #region Constructor tests
        [Fact]
        public void Ctor_Should_Throw_ArgumentNullException_If_RoleManager_Is_Null()
        {
            RoleManager<IdentityRole> roleManager = null;

            var ex = Assert.Throws<ArgumentNullException>(() => new RoleFactory(roleManager));
            Assert.Equal(nameof(roleManager), ex.ParamName);
        }
        #endregion

        #region Methods tests
        [Fact]
        public async Task Administrator_Should_Return_A_Role_Named_Administrator()
        {
            var roleStoreMock = new Mock<IRoleStore<IdentityRole>>();
            roleStoreMock
                .Setup(r => r.CreateAsync(It.IsAny<IdentityRole>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(IdentityResult.Success);

            var roleManager = Mocks.RoleManagerMockFactory.BuildRoleManager(roleStoreMock.Object);

            var roleFactory = new RoleFactory(roleManager);

            var administrator = await roleFactory.Administrator();
            Assert.Equal(AuthenticationDefaults.AdministratorRole, administrator.Name);
        }

        [Fact]
        public async Task Customer_Should_Return_A_Role_Named_Customer()
        {
            var roleStoreMock = new Mock<IRoleStore<IdentityRole>>();
            roleStoreMock
                .Setup(r => r.CreateAsync(It.IsAny<IdentityRole>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(IdentityResult.Success);

            var roleManager = Mocks.RoleManagerMockFactory.BuildRoleManager(roleStoreMock.Object);

            var roleFactory = new RoleFactory(roleManager);

            var administrator = await roleFactory.Customer();
            Assert.Equal(AuthenticationDefaults.CustomerRole, administrator.Name);
        }
        #endregion
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Security.Claims;
using Wilcommerce.Auth.Models;
using Xunit;

namespace Wilcommerce.Auth.Test.Extensions
{
    public class UserManagerExtensionsTest
    {
        [Fact]
        public void UserManagerExtensions_GetUserFullName_Should_Throw_ArgumentNullException_If_UserManager_Is_Null()
        {
            UserManager<User> userManager = null;
            ClaimsPrincipal principal = new ClaimsPrincipal();

            var ex = Assert.Throws<ArgumentNullException>(() => UserManagerExtensions.GetUserFullName(userManager, principal));
            Assert.Equal(nameof(userManager), ex.ParamName);
        }

        [Fact]
        public void UserManagerExtensions_GetUserFullName_Should_Throw_ArgumentNullException_If_Principal_Is_Null()
        {
            UserManager<User> userManager = Mocks.UserManagerMockFactory.BuildUserManager();
            ClaimsPrincipal principal = null;

            var ex = Assert.Throws<ArgumentNullException>(() => UserManagerExtensions.GetUserFullName(userManager, principal));
            Assert.Equal(nameof(principal), ex.ParamName);
        }

        [Fact]
        public void UserManagerExtensions_GetUserFullName_Should_Return_GivenName_Principal_Value()
        {
            string fullName = "Administrator";

            UserManager<User> userManager = Mocks.UserManagerMockFactory.BuildUserManager();
            ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity[]
            {
                new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.GivenName, fullName)
                })
            });

            string userFullName = UserManagerExtensions.GetUserFullName(userManager, principal);
            Assert.Equal(fullName, userFullName);
        }

        [Fact]
        public void UserManagerExtensions_GetUserFullName_Should_Return_UserName_If_GivenName_Principal_Is_Null()
        {
            string userName = "admin@wilcommerce.com";

            var optionsAccessorMock = new Mock<IOptions<IdentityOptions>>();
            optionsAccessorMock.Setup(o => o.Value).Returns(
                new IdentityOptions
                {
                    ClaimsIdentity = new ClaimsIdentityOptions { UserNameClaimType = ClaimTypes.Name }
                });

            UserManager<User> userManager = Mocks.UserManagerMockFactory.BuildUserManager(optionsAccessor: optionsAccessorMock.Object);

            ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity[]
            {
                new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userName)
                })
            });

            string userNameReturned = UserManagerExtensions.GetUserFullName(userManager, principal);
            Assert.Equal(userName, userNameReturned);
        }
    }
}

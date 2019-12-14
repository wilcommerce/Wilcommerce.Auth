using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wilcommerce.Auth.Models;
using Wilcommerce.Auth.Services;
using Xunit;

namespace Wilcommerce.Auth.Test.Services
{
    public class TokenGeneratorTest
    {
        #region Constructor test
        [Fact]
        public void Ctor_Should_Throw_ArgumentException_If_UserManager_Is_Null()
        {
            UserManager<User> userManager = null;

            var ex = Assert.Throws<ArgumentNullException>(() => new TokenGenerator(userManager));
            Assert.Equal(nameof(userManager), ex.ParamName);
        }
        #endregion

        #region GenerateEmailConfirmationTokenForUser tests
        [Fact]
        public async Task GenerateEmailConfirmationTokenForUser_Should_Throw_ArgumentNullException_If_User_Is_Null()
        {
            UserManager<User> userManager = Mocks.UserManagerMockFactory.BuildUserManager();
            User user = null;

            var tokenGenerator = new TokenGenerator(userManager);
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => tokenGenerator.GenerateEmailConfirmationTokenForUser(user));

            Assert.Equal(nameof(user), ex.ParamName);
        }

        [Fact]
        public async Task GenerateEmailConfirmationTokenForUser_Should_Generate_A_Not_Empty_String()
        {
            string emailTokenProviderName = "emailTokenProvider";
            string emailToken = Guid.NewGuid().ToString();

            var userTwoFactorTokenProviderMock = new Mock<IUserTwoFactorTokenProvider<Auth.Models.User>>();
            userTwoFactorTokenProviderMock.Setup(p => p.GenerateAsync(It.IsAny<string>(), It.IsAny<UserManager<User>>(), It.IsAny<User>()))
                .Returns(Task.FromResult(emailToken));

            var identityOptions = new IdentityOptions
            {
                Tokens = new TokenOptions
                {
                    EmailConfirmationTokenProvider = emailTokenProviderName,
                    ProviderMap = new Dictionary<string, TokenProviderDescriptor>
                    {
                        { "Default", new TokenProviderDescriptor(userTwoFactorTokenProviderMock.Object.GetType()) { ProviderInstance = userTwoFactorTokenProviderMock.Object } },
                        { emailTokenProviderName, new TokenProviderDescriptor(userTwoFactorTokenProviderMock.Object.GetType()) { ProviderInstance = userTwoFactorTokenProviderMock.Object } }
                    }
                }
            };

            var optionsAccessorMock = new Mock<IOptions<IdentityOptions>>();
            optionsAccessorMock.Setup(o => o.Value)
                .Returns(identityOptions);

            UserManager<User> userManager = Mocks.UserManagerMockFactory.BuildUserManager(optionsAccessor: optionsAccessorMock.Object);
            User user = User.CreateAsAdministrator("admin", "admin@admin.com", true);

            var tokenGenerator = new TokenGenerator(userManager);

            string token = await tokenGenerator.GenerateEmailConfirmationTokenForUser(user);

            Assert.False(string.IsNullOrWhiteSpace(token));
            Assert.Equal(emailToken, token);
        }
        #endregion

        #region GeneratePasswordRecoveryTokenForUser tests
        [Fact]
        public async Task GeneratePasswordRecoveryTokenForUser_Should_Throw_ArgumentNullException_If_User_Is_Null()
        {
            UserManager<User> userManager = Mocks.UserManagerMockFactory.BuildUserManager();
            User user = null;

            var tokenGenerator = new TokenGenerator(userManager);
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => tokenGenerator.GeneratePasswordRecoveryTokenForUser(user));

            Assert.Equal(nameof(user), ex.ParamName);
        }

        [Fact]
        public async Task GeneratePasswordRecoveryTokenForUser_Should_Generate_A_Not_Empty_String()
        {
            string passworResetTokenProviderName = "passwordResetTokenProvider";
            string passwordToken = Guid.NewGuid().ToString();

            var userTwoFactorTokenProviderMock = new Mock<IUserTwoFactorTokenProvider<Auth.Models.User>>();
            userTwoFactorTokenProviderMock.Setup(p => p.GenerateAsync(It.IsAny<string>(), It.IsAny<UserManager<User>>(), It.IsAny<User>()))
                .Returns(Task.FromResult(passwordToken));

            var identityOptions = new IdentityOptions
            {
                Tokens = new TokenOptions
                {
                    EmailConfirmationTokenProvider = passworResetTokenProviderName,
                    ProviderMap = new Dictionary<string, TokenProviderDescriptor>
                    {
                        { "Default", new TokenProviderDescriptor(userTwoFactorTokenProviderMock.Object.GetType()) { ProviderInstance = userTwoFactorTokenProviderMock.Object } },
                        { passworResetTokenProviderName, new TokenProviderDescriptor(userTwoFactorTokenProviderMock.Object.GetType()) { ProviderInstance = userTwoFactorTokenProviderMock.Object } }
                    }
                }
            };

            var optionsAccessorMock = new Mock<IOptions<IdentityOptions>>();
            optionsAccessorMock.Setup(o => o.Value)
                .Returns(identityOptions);

            UserManager<User> userManager = Mocks.UserManagerMockFactory.BuildUserManager(optionsAccessor: optionsAccessorMock.Object);
            User user = User.CreateAsAdministrator("admin", "admin@admin.com", true);

            var tokenGenerator = new TokenGenerator(userManager);

            string token = await tokenGenerator.GeneratePasswordRecoveryTokenForUser(user);

            Assert.False(string.IsNullOrWhiteSpace(token));
            Assert.Equal(passwordToken, token);
        }
        #endregion
    }
}

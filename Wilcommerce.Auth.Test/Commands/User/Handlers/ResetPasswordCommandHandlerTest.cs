using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Wilcommerce.Auth.Commands.User.Handlers;
using Wilcommerce.Core.Infrastructure;
using Xunit;
using System.Threading.Tasks;
using Wilcommerce.Auth.Commands.User;
using Microsoft.Extensions.Options;

namespace Wilcommerce.Auth.Test.Commands.User.Handlers
{
    public class ResetPasswordCommandHandlerTest
    {
        #region Constructor tests
        [Fact]
        public void Ctor_Should_Throw_ArgumentNullException_If_UserManager_Is_Null()
        {
            UserManager<Auth.Models.User> userManager = null;
            IEventBus eventBus = new Mock<IEventBus>().Object;

            var ex = Assert.Throws<ArgumentNullException>(() => new ResetPasswordCommandHandler(userManager, eventBus));
            Assert.Equal(nameof(userManager), ex.ParamName);
        }

        [Fact]
        public void Ctor_Should_Throw_ArgumentNullException_If_EventBus_Is_Null()
        {
            UserManager<Auth.Models.User> userManager = Mocks.UserManagerMockFactory.BuildUserManager();
            IEventBus eventBus = null;

            var ex = Assert.Throws<ArgumentNullException>(() => new ResetPasswordCommandHandler(userManager, eventBus));
            Assert.Equal(nameof(eventBus), ex.ParamName);
        }
        #endregion

        #region Handle tests
        [Fact]
        public void Handle_Should_Throw_ApplicationException_If_Operation_Does_Not_Succeeded()
        {
            var userModel = Auth.Models.User.CreateAsAdministrator("admin", "admin@admin.com", true);

            var userId = Guid.Parse(userModel.Id);
            string resetToken = Guid.NewGuid().ToString();
            string newPassword = "newpassword";
            var command = new ResetPasswordCommand(userId, resetToken, newPassword);

            var userTwoFactorTokenProviderMock = new Mock<IUserTwoFactorTokenProvider<Auth.Models.User>>();

            var identityOptions = new IdentityOptions
            {
                Tokens = new TokenOptions
                {
                    ProviderMap = new Dictionary<string, TokenProviderDescriptor>
                    {
                        { "Default", new TokenProviderDescriptor(userTwoFactorTokenProviderMock.Object.GetType()) { ProviderInstance = userTwoFactorTokenProviderMock.Object } },
                        { resetToken, new TokenProviderDescriptor(userTwoFactorTokenProviderMock.Object.GetType()) { ProviderInstance = userTwoFactorTokenProviderMock.Object } }
                    }
                }
            };

            var optionsAccessorMock = new Mock<IOptions<IdentityOptions>>();
            optionsAccessorMock.Setup(o => o.Value)
                .Returns(identityOptions);

            var storeMock = new Mock<IUserPasswordStore<Auth.Models.User>>();
            storeMock.Setup(s => s.FindByIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(userModel));

            storeMock.Setup(s => s.UpdateAsync(It.IsAny<Auth.Models.User>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(IdentityResult.Failed(new IdentityError[0])));

            storeMock.Setup(s => s.SetPasswordHashAsync(It.IsAny<Auth.Models.User>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(0));

            var passwordHasherMock = new Mock<IPasswordHasher<Auth.Models.User>>();
            passwordHasherMock.Setup(p => p.HashPassword(It.IsAny<Auth.Models.User>(), It.IsAny<string>()))
                .Returns($"{typeof(Auth.Models.User).ToString()}{newPassword}");

            var userManager = Mocks.UserManagerMockFactory.BuildUserManager(store: storeMock.Object, optionsAccessor: optionsAccessorMock.Object );
            var eventBus = new Mock<IEventBus>().Object;

            var handler = new ResetPasswordCommandHandler(userManager, eventBus);

            var ex = Assert.Throws<AggregateException>(() => handler.Handle(command).Wait());
            Assert.Equal(typeof(ApplicationException), ex.InnerException.GetType());
            Assert.Equal("There was an error resetting the user password", ex.InnerException.Message);
        }

        [Fact]
        public void Handle_Should_Reset_User_Password_With_The_Specified_Value()
        {
            var userModel = Auth.Models.User.CreateAsAdministrator("admin", "admin@admin.com", true);

            var userId = Guid.Parse(userModel.Id);
            string resetToken = Guid.NewGuid().ToString();
            string newPassword = "newpassword";
            var command = new ResetPasswordCommand(userId, resetToken, newPassword);

            string newPasswordHash = $"{typeof(Auth.Models.User).ToString()}{newPassword}";

            var userTwoFactorTokenProviderMock = new Mock<IUserTwoFactorTokenProvider<Auth.Models.User>>();
            userTwoFactorTokenProviderMock.Setup(p => p.ValidateAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<UserManager<Auth.Models.User>>(), It.IsAny<Auth.Models.User>()))
                .Returns(Task.FromResult(true));

            var identityOptions = new IdentityOptions
            {
                Tokens = new TokenOptions
                {
                    ProviderMap = new Dictionary<string, TokenProviderDescriptor>
                    {
                        { "Default", new TokenProviderDescriptor(userTwoFactorTokenProviderMock.Object.GetType()) { ProviderInstance = userTwoFactorTokenProviderMock.Object } },
                        { resetToken, new TokenProviderDescriptor(userTwoFactorTokenProviderMock.Object.GetType()) { ProviderInstance = userTwoFactorTokenProviderMock.Object } }
                    }
                }
            };

            var optionsAccessorMock = new Mock<IOptions<IdentityOptions>>();
            optionsAccessorMock.Setup(o => o.Value)
                .Returns(identityOptions);

            var storeMock = new Mock<IUserPasswordStore<Auth.Models.User>>();
            storeMock.Setup(s => s.FindByIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(userModel));

            storeMock.Setup(s => s.UpdateAsync(It.IsAny<Auth.Models.User>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(IdentityResult.Success));

            storeMock.Setup(s => s.SetPasswordHashAsync(It.IsAny<Auth.Models.User>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Returns(Task.Factory.StartNew(() => { userModel.PasswordHash = newPasswordHash; }));

            var passwordHasherMock = new Mock<IPasswordHasher<Auth.Models.User>>();
            passwordHasherMock.Setup(p => p.HashPassword(It.IsAny<Auth.Models.User>(), It.IsAny<string>()))
                .Returns(newPasswordHash);

            var userManager = Mocks.UserManagerMockFactory.BuildUserManager(store: storeMock.Object, optionsAccessor: optionsAccessorMock.Object, passwordHasher: passwordHasherMock.Object);
            var eventBus = new Mock<IEventBus>().Object;

            var handler = new ResetPasswordCommandHandler(userManager, eventBus);

            handler.Handle(command).Wait();

            Assert.Equal(newPasswordHash, userModel.PasswordHash);
        }
        #endregion
    }
}

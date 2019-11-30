using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Wilcommerce.Auth.Commands.User;
using Wilcommerce.Auth.Commands.User.Handlers;
using Wilcommerce.Core.Infrastructure;
using Xunit;

namespace Wilcommerce.Auth.Test.Commands.User.Handlers
{
    public class EnableUserCommandHandlerTest
    {
        #region Constructor tests
        [Fact]
        public void Ctor_Should_Throw_ArgumentNullException_If_UserManager_Is_Null()
        {
            UserManager<Auth.Models.User> userManager = null;
            IEventBus eventBus = new Mock<IEventBus>().Object;

            var ex = Assert.Throws<ArgumentNullException>(() => new EnableUserCommandHandler(userManager, eventBus));
            Assert.Equal(nameof(userManager), ex.ParamName);
        }

        [Fact]
        public void Ctor_Should_Throw_ArgumentNullException_If_EventBus_Is_Null()
        {
            UserManager<Auth.Models.User> userManager = Mocks.UserManagerMockFactory.BuildUserManager();
            IEventBus eventBus = null;

            var ex = Assert.Throws<ArgumentNullException>(() => new EnableUserCommandHandler(userManager, eventBus));
            Assert.Equal(nameof(eventBus), ex.ParamName);
        }
        #endregion

        #region Handle tests
        [Fact]
        public void Handle_Should_Throw_ApplicationException_If_Operation_Does_Not_Succeeded()
        {
            var userModel = Auth.Models.User.CreateAsAdministrator("name", "admin@admin.com", false);
            var userId = Guid.Parse(userModel.Id);

            var command = new EnableUserCommand(userId);

            var storeMock = new Mock<IUserStore<Auth.Models.User>>();
            storeMock.Setup(s => s.FindByIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(userModel));

            storeMock.Setup(s => s.UpdateAsync(It.IsAny<Auth.Models.User>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(IdentityResult.Failed(new IdentityError[0])));

            var userManager = Mocks.UserManagerMockFactory.BuildUserManager(store: storeMock.Object);
            var eventBus = new Mock<IEventBus>().Object;

            var handler = new EnableUserCommandHandler(userManager, eventBus);

            var ex = Assert.Throws<AggregateException>(() => handler.Handle(command).Wait());
            Assert.Equal(typeof(ApplicationException), ex.InnerException.GetType());
            Assert.Equal("Error while enabling the user", ex.InnerException.Message);
        }

        [Fact]
        public void Handle_Should_Change_User_Info_Correctly_With_Specified_Values()
        {
            var userModel = Auth.Models.User.CreateAsAdministrator("name", "admin@admin.com", false);
            var userId = Guid.Parse(userModel.Id);

            var command = new EnableUserCommand(userId);

            var storeMock = new Mock<IUserStore<Auth.Models.User>>();
            storeMock.Setup(s => s.FindByIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(userModel));

            storeMock.Setup(s => s.UpdateAsync(It.IsAny<Auth.Models.User>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(IdentityResult.Success));

            var userManager = Mocks.UserManagerMockFactory.BuildUserManager(store: storeMock.Object);
            var eventBus = new Mock<IEventBus>().Object;

            var handler = new EnableUserCommandHandler(userManager, eventBus);
            handler.Handle(command).Wait();

            Assert.True(userModel.IsActive);
        }
        #endregion
    }
}

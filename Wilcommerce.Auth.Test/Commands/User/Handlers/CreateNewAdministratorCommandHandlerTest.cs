using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Wilcommerce.Auth.Commands.User;
using Wilcommerce.Auth.Commands.User.Handlers;
using Wilcommerce.Auth.Services.Interfaces;
using Wilcommerce.Auth.Test.Mocks.CreateNewAdministratorCommandHandlerTest;
using Wilcommerce.Core.Infrastructure;
using Xunit;

namespace Wilcommerce.Auth.Test.Commands.User.Handlers
{
    public class CreateNewAdministratorCommandHandlerTest
    {
        #region Constructor tests
        [Fact]
        public void Ctor_Should_Throw_ArgumentNullException_If_UserManager_Is_Null()
        {
            UserManager<Auth.Models.User> userManager = null;
            IEventBus eventBus = new Mock<IEventBus>().Object;
            IRoleFactory roleFactory = new Mock<IRoleFactory>().Object;

            var ex = Assert.Throws<ArgumentNullException>(() => new CreateNewAdministratorCommandHandler(userManager, eventBus, roleFactory));
            Assert.Equal(nameof(userManager), ex.ParamName);
        }

        [Fact]
        public void Ctor_Should_Throw_ArgumentNullException_If_EventBus_Is_Null()
        {
            UserManager<Auth.Models.User> userManager = Mocks.UserManagerMockFactory.BuildUserManager();
            IEventBus eventBus = null;
            IRoleFactory roleFactory = new Mock<IRoleFactory>().Object;

            var ex = Assert.Throws<ArgumentNullException>(() => new CreateNewAdministratorCommandHandler(userManager, eventBus, roleFactory));
            Assert.Equal(nameof(eventBus), ex.ParamName);
        }

        [Fact]
        public void Ctor_Should_Throw_ArgumentNullException_If_RoleFactory_Is_Null()
        {
            UserManager<Auth.Models.User> userManager = Mocks.UserManagerMockFactory.BuildUserManager();
            IEventBus eventBus = new Mock<IEventBus>().Object;
            IRoleFactory roleFactory = null;

            var ex = Assert.Throws<ArgumentNullException>(() => new CreateNewAdministratorCommandHandler(userManager, eventBus, roleFactory));
            Assert.Equal(nameof(roleFactory), ex.ParamName);
        }
        #endregion

        #region Handle tests
        [Fact]
        public void Handle_Should_Throw_InvalidOperationException_If_Operation_Does_Not_Succeeded()
        {
            var error = new IdentityError { Description = "error" };

            string name = "admin";
            string email = "admin@admin.com";
            string password = "password";
            bool isActive = true;

            var storeMock = new Mock<IUserPasswordStore<Auth.Models.User>>();
            storeMock.Setup(s => s.CreateAsync(It.IsAny<Auth.Models.User>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(IdentityResult.Failed(error)));

            storeMock.Setup(s => s.SetPasswordHashAsync(It.IsAny<Auth.Models.User>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(0));

            var passwordHasherMock = new Mock<IPasswordHasher<Auth.Models.User>>();
            passwordHasherMock.Setup(p => p.HashPassword(It.IsAny<Auth.Models.User>(), It.IsAny<string>()))
                .Returns($"{typeof(Auth.Models.User).ToString()}{password}");

            UserManager<Auth.Models.User> userManager = Mocks.UserManagerMockFactory.BuildUserManager(store: storeMock.Object, passwordHasher: passwordHasherMock.Object);
            IEventBus eventBus = new Mock<IEventBus>().Object;

            var roleFactoryMock = new Mock<IRoleFactory>();
            roleFactoryMock.Setup(r => r.Administrator())
                .Returns(Task.FromResult(new IdentityRole(AuthenticationDefaults.AdministratorRole)));

            IRoleFactory roleFactory = roleFactoryMock.Object;

            var command = new CreateNewAdministratorCommand(name, email, password, isActive);

            var handler = new CreateNewAdministratorCommandHandler(userManager, eventBus, roleFactory);

            var ex = Assert.Throws<AggregateException>(() => handler.Handle(command).Wait());
            Assert.Equal(typeof(InvalidOperationException), ex.InnerException.GetType());
            Assert.Equal(error.Description, ex.InnerException.Message);
        }

        [Fact]
        public void Handle_Should_Create_A_New_User_With_Specified_Values()
        {
            string name = "admin";
            string email = "admin@admin.com";
            string password = "password";
            bool isActive = true;

            var users = new List<Auth.Models.User>();

            var store = new UserStoreMock();

            string passwordHash = $"{typeof(Auth.Models.User).ToString()}{password}";
            var passwordHasherMock = new Mock<IPasswordHasher<Auth.Models.User>>();
            passwordHasherMock.Setup(p => p.HashPassword(It.IsAny<Auth.Models.User>(), It.IsAny<string>()))
                .Returns(passwordHash);

            UserManager<Auth.Models.User> userManager = Mocks.UserManagerMockFactory.BuildUserManager(store: store, passwordHasher: passwordHasherMock.Object);
            IEventBus eventBus = new Mock<IEventBus>().Object;

            var roleFactoryMock = new Mock<IRoleFactory>();
            roleFactoryMock.Setup(r => r.Administrator())
                .Returns(Task.FromResult(new IdentityRole(AuthenticationDefaults.AdministratorRole)));

            IRoleFactory roleFactory = roleFactoryMock.Object;

            var command = new CreateNewAdministratorCommand(name, email, password, isActive);

            var handler = new CreateNewAdministratorCommandHandler(userManager, eventBus, roleFactory);
            handler.Handle(command).Wait();

            Assert.NotEmpty(UserStoreMock.FakeUsersStore);

            var user = UserStoreMock.FakeUsersStore.First();
            Assert.Equal(name, user.Name);
            Assert.Equal(email, user.Email);
            Assert.Equal(passwordHash, user.PasswordHash);
        }
        #endregion
    }
}

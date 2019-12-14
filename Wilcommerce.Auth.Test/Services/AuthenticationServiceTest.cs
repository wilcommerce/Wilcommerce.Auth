using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Wilcommerce.Auth.Models;
using Wilcommerce.Auth.ReadModels;
using Wilcommerce.Auth.Services;
using Wilcommerce.Core.Infrastructure;
using Xunit;

namespace Wilcommerce.Auth.Test.Services
{
    public class AuthenticationServiceTest
    {
        #region Constructor tests
        [Fact]
        public void Ctor_Should_Throw_ArgumentNullException_If_AuthDatabase_Is_Null()
        {
            IAuthDatabase authDatabase = null;
            IEventBus eventBus = new Mock<IEventBus>().Object;
            SignInManager<User> signInManager = Mocks.SignInManagerMockFactory.BuildSignInManager();

            var ex = Assert.Throws<ArgumentNullException>(() => new AuthenticationService(authDatabase, eventBus, signInManager));
            Assert.Equal(nameof(authDatabase), ex.ParamName);
        }

        [Fact]
        public void Ctor_Should_Throw_ArgumentNullException_If_EventBus_Is_Null()
        {
            IAuthDatabase authDatabase = new Mock<IAuthDatabase>().Object;
            IEventBus eventBus = null;
            SignInManager<User> signInManager = Mocks.SignInManagerMockFactory.BuildSignInManager();

            var ex = Assert.Throws<ArgumentNullException>(() => new AuthenticationService(authDatabase, eventBus, signInManager));
            Assert.Equal(nameof(eventBus), ex.ParamName);
        }

        [Fact]
        public void Ctor_Should_Throw_ArgumentNullException_If_SignInManager_Is_Null()
        {
            IAuthDatabase authDatabase = new Mock<IAuthDatabase>().Object;
            IEventBus eventBus = new Mock<IEventBus>().Object;
            SignInManager<User> signInManager = null;

            var ex = Assert.Throws<ArgumentNullException>(() => new AuthenticationService(authDatabase, eventBus, signInManager));
            Assert.Equal(nameof(signInManager), ex.ParamName);
        }
        #endregion
    }
}

using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using Wilcommerce.Auth.Models;
using Wilcommerce.Core.Common.Domain.Models;
using Xunit;

namespace Wilcommerce.Auth.Test.Models
{
    public class UserTokenTest
    {
        [Fact]
        public void PasswordRecovery_Should_Throw_ArgumentNullException_If_User_IsNull()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => UserToken.PasswordRecovery(null, "", DateTime.Now));
            Assert.Equal("user", ex.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void PasswordRecovery_Should_Throw_ArgumentNullException_If_Token_IsEmpty(string value)
        {
            var user = Core.Common.Domain.Models.User.CreateAsAdministrator("Admin", "admin@admin.com", "password", new Mock<IPasswordHasher<Core.Common.Domain.Models.User>>().Object);

            var ex = Assert.Throws<ArgumentNullException>(() => UserToken.PasswordRecovery(user, value, DateTime.Now));
            Assert.Equal("token", ex.ParamName);
        }

        [Fact]
        public void PasswordRecovery_Should_Throw_ArgumentException_If_ExpirationDate_IsPreviousThan_Now()
        {
            var user = Core.Common.Domain.Models.User.CreateAsAdministrator("Admin", "admin@admin.com", "password", new Mock<IPasswordHasher<Core.Common.Domain.Models.User>>().Object);
            string token = "token";

            var ex = Assert.Throws<ArgumentException>(() => UserToken.PasswordRecovery(user, token, DateTime.Now.AddDays(-1)));
            Assert.Equal("expirationDate", ex.ParamName);
        }

        [Fact]
        public void PasswordRecovery_Should_Create_A_PasswordRecovery_Token()
        {
            var user = Core.Common.Domain.Models.User.CreateAsAdministrator("Admin", "admin@admin.com", "password", new Mock<IPasswordHasher<Core.Common.Domain.Models.User>>().Object);
            string token = "token";
            var expirationDate = DateTime.Now.AddDays(10);

            var userToken = UserToken.PasswordRecovery(user, token, expirationDate);
            Assert.Equal(TokenTypes.PasswordRecovery, userToken.TokenType);
        }

        [Fact]
        public void Registration_Should_Throw_ArgumentNullException_If_User_IsNull()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => UserToken.Registration(null, "", DateTime.Now));
            Assert.Equal("user", ex.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Registration_Should_Throw_ArgumentNullException_If_Token_IsEmpty(string value)
        {
            var user = Core.Common.Domain.Models.User.CreateAsAdministrator("Admin", "admin@admin.com", "password", new Mock<IPasswordHasher<Core.Common.Domain.Models.User>>().Object);

            var ex = Assert.Throws<ArgumentNullException>(() => UserToken.Registration(user, value, DateTime.Now));
            Assert.Equal("token", ex.ParamName);
        }

        [Fact]
        public void Registration_Should_Throw_ArgumentException_If_ExpirationDate_IsPreviousThan_Now()
        {
            var user = Core.Common.Domain.Models.User.CreateAsAdministrator("Admin", "admin@admin.com", "password", new Mock<IPasswordHasher<Core.Common.Domain.Models.User>>().Object);
            string token = "token";

            var ex = Assert.Throws<ArgumentException>(() => UserToken.Registration(user, token, DateTime.Now.AddDays(-1)));
            Assert.Equal("expirationDate", ex.ParamName);
        }

        [Fact]
        public void Registration_Should_Create_A_Registration_Token()
        {
            var user = Core.Common.Domain.Models.User.CreateAsAdministrator("Admin", "admin@admin.com", "password", new Mock<IPasswordHasher<Core.Common.Domain.Models.User>>().Object);
            string token = "token";
            var expirationDate = DateTime.Now.AddDays(10);

            var userToken = UserToken.Registration(user, token, expirationDate);
            Assert.Equal(TokenTypes.Registration, userToken.TokenType);
        }

        [Fact]
        public void SetAsExpired_Should_Throw_InvalidOperationException_If_Token_Is_Already_Expired()
        {
            var user = Core.Common.Domain.Models.User.CreateAsAdministrator("Admin", "admin@admin.com", "password", new Mock<IPasswordHasher<Core.Common.Domain.Models.User>>().Object);
            string token = "token";
            var expirationDate = DateTime.Now.AddDays(10);

            var userToken = UserToken.Registration(user, token, expirationDate);
            userToken.SetAsExpired();

            var ex = Assert.Throws<InvalidOperationException>(() => userToken.SetAsExpired());
            Assert.Equal($"Token already expired on {userToken.ExpirationDate.ToString()}", ex.Message);
        }

        [Fact]
        public void SetAsExpired_Should_Set_ExpirationDate_To_Today()
        {
            var user = Core.Common.Domain.Models.User.CreateAsAdministrator("Admin", "admin@admin.com", "password", new Mock<IPasswordHasher<Core.Common.Domain.Models.User>>().Object);
            string token = "token";
            var expirationDate = DateTime.Now.AddDays(10);

            var userToken = UserToken.Registration(user, token, expirationDate);
            userToken.SetAsExpired();

            Assert.True(userToken.IsExpired);
            Assert.Equal(DateTime.Now.ToString("yyyy-MM-dd"), userToken.ExpirationDate.ToString("yyyy-MM-dd"));
        }
    }
}

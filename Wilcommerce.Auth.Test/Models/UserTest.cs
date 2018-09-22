using System;
using Wilcommerce.Auth.Models;
using Xunit;

namespace Wilcommerce.Auth.Test.Models
{
    public class UserTest
    {
        #region Administrator tests
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void AdministratorFactory_Should_Throw_ArgumentNull_Exception_If_Name_IsEmpty(string value)
        {
            var ex = Assert.Throws<ArgumentException>(() => User.CreateAsAdministrator(
                value,
                "admin@email.com",
                true));

            Assert.Equal("name", ex.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void AdministratorFactory_Should_Throw_ArgumentNull_Exception_If_Email_IsEmpty(string value)
        {
            var ex = Assert.Throws<ArgumentException>(() => User.CreateAsAdministrator(
                "Administrator",
                value,
                true));

            Assert.Equal("email", ex.ParamName);
        }

        #endregion

        #region Customer tests
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void CustomerFactory_Should_Throw_ArgumentNull_Exception_If_Name_IsEmpty(string value)
        {
            var ex = Assert.Throws<ArgumentException>(() => User.CreateAsCustomer(
                value,
                "customer@email.com"));

            Assert.Equal("name", ex.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void CustomerFactory_Should_Throw_ArgumentNull_Exception_If_Email_IsEmpty(string value)
        {
            var ex = Assert.Throws<ArgumentException>(() => User.CreateAsCustomer(
                "Customer",
                value));

            Assert.Equal("email", ex.ParamName);
        }
        #endregion

        [Fact]
        public void Enable_Should_Active_User_And_Set_Date_To_Null()
        {
            var user = User.CreateAsAdministrator(
                "Admin",
                "admin@email.com",
                false);

            user.Enable();

            Assert.True(user.IsActive);
            Assert.Null(user.DisabledOn);
        }

        [Fact]
        public void Disable_Should_Set_Date_To_Now()
        {
            var user = User.CreateAsAdministrator(
                "Admin",
                "admin@email.com",
                true);

            user.Disable();

            Assert.False(user.IsActive);
            Assert.Equal(DateTime.Today, ((DateTime)user.DisabledOn).Date);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ChangeName_Should_Throw_ArgumentNullException_If_Name_IsEmpty(string value)
        {
            var user = User.CreateAsAdministrator(
                "Admin",
                "admin@email.com",
                true);

            var ex = Assert.Throws<ArgumentException>(() => user.ChangeName(value));
            Assert.Equal("name", ex.ParamName);
        }

        [Fact]
        public void SetProfileImage_Should_Throw_ArgumentNullException_If_Image_IsNull()
        {
            var user = User.CreateAsAdministrator(
                "Admin",
                "admin@email.com",
                true);

            var ex = Assert.Throws<ArgumentNullException>(() => user.SetProfileImage(null));
            Assert.Equal("profileImage", ex.ParamName);
        }

        [Fact]
        public void Constructor_Should_Initialize_Empty_ProfileImage()
        {
            var admin = User.CreateAsAdministrator(
                "Administrator",
                "admin@email.com",
                true);

            Assert.NotNull(admin.ProfileImage);
        }
    }
}

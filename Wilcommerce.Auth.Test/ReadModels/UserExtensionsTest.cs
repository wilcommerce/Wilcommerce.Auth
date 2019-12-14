using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wilcommerce.Auth.Models;
using Wilcommerce.Auth.ReadModels;
using Xunit;

namespace Wilcommerce.Auth.Test.ReadModels
{
    public class UserExtensionsTest
    {
        [Fact]
        public void UserExtensions_Actives_Should_Throw_ArgumentNullException_If_Users_Is_Null()
        {
            IQueryable<User> users = null;

            var ex = Assert.Throws<ArgumentNullException>(() => UserExtensions.Actives(users));
            Assert.Equal(nameof(users), ex.ParamName);
        }

        [Fact]
        public void UserExtensions_Actives_Should_Return_Only_Active_Users()
        {
            IQueryable<User> users = new User[]
            {
                User.CreateAsAdministrator("Admin1", "admin1@wilcommerce.com", true),
                User.CreateAsAdministrator("Admin2", "admin2@wilcommerce.com", true),
                User.CreateAsAdministrator("Admin3", "admin3@wilcommerce.com", false)
            }.AsQueryable();

            var activeUsers = UserExtensions.Actives(users);

            Assert.True(activeUsers.All(u => u.IsActive));
        }

        [Fact]
        public void UserExtensions_WithUsername_Should_Throw_ArgumentNullException_If_Users_Is_Null()
        {
            IQueryable<User> users = null;
            string username = "username";

            var ex = Assert.Throws<ArgumentNullException>(() => UserExtensions.WithUsername(users, username));
            Assert.Equal(nameof(users), ex.ParamName);
        }

        [Fact]
        public void UserExtensions_WithUsername_Should_Return_User_Having_The_Specified_Username()
        {
            IQueryable<User> users = new User[]
            {
                User.CreateAsAdministrator("Admin1", "admin1@wilcommerce.com", true),
                User.CreateAsAdministrator("Admin2", "admin2@wilcommerce.com", true),
                User.CreateAsAdministrator("Admin3", "admin3@wilcommerce.com", false)
            }.AsQueryable();

            string username = "admin1@wilcommerce.com";

            var usersWithUsername = UserExtensions.WithUsername(users, username);
            Assert.True(usersWithUsername.All(u => u.UserName == username));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wilcommerce.Auth.Services;
using Wilcommerce.Auth.Services.Interfaces;
using Wilcommerce.Core.Common.Domain.Models;
using Xunit;

namespace Wilcommerce.Auth.Test.Services
{
    public class IdentityFactoryTest
    {
        private IIdentityFactory _factory;

        public IdentityFactoryTest()
        {
            _factory = new IdentityFactory();
        }

        [Fact]
        public void CreateIdentity_Identity_Name_Must_Match_User_Email()
        {
            var user = User.CreateAsAdministrator("User", "admin@admin.com", "1234");
            var principal = _factory.CreateIdentity(user);

            Assert.Equal(user.Email, principal.Identity.Name);
        }

        [Fact]
        public void AdministratorUser_Must_Have_Administrator_As_Role()
        {
            var user = User.CreateAsAdministrator("User", "admin@admin.com", "1234");
            var principal = _factory.CreateIdentity(user);

            Assert.True(principal.IsInRole(AuthenticationDefaults.AdministratorRole));
        }

        [Fact]
        public void CustomerUser_Must_Have_Customer_As_Role()
        {
            var user = User.CreateAsCustomer("Customer", "customer@customer.com", "1234");
            var principal = _factory.CreateIdentity(user);

            Assert.True(principal.IsInRole(AuthenticationDefaults.CustomerRole));
        }
    }
}

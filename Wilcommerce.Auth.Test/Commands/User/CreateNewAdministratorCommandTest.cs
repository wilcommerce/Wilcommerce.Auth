using System;
using System.Collections.Generic;
using System.Text;
using Wilcommerce.Auth.Commands.User;
using Xunit;

namespace Wilcommerce.Auth.Test.Commands.User
{
    public class CreateNewAdministratorCommandTest
    {
        [Fact]
        public void CreateNewAdministratorCommand_Ctor_Should_Set_Arguments_Correctly()
        {
            string name = "admin";
            string email = "admin@wilcommerce.com";
            string password = "password";
            bool isActive = true;

            var command = new CreateNewAdministratorCommand(name, email, password, isActive);

            Assert.Equal(name, command.Name);
            Assert.Equal(email, command.Email);
            Assert.Equal(password, command.Password);
            Assert.Equal(isActive, command.IsActive);
        }
    }
}

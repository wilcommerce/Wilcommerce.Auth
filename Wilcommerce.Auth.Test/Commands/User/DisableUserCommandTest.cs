using System;
using Wilcommerce.Auth.Commands.User;
using Xunit;

namespace Wilcommerce.Auth.Test.Commands.User
{
    public class DisableUserCommandTest
    {
        [Fact]
        public void DisableUserCommand_Ctor_Should_Set_Arguments_Correctly()
        {
            Guid userId = Guid.NewGuid();
            var command = new DisableUserCommand(userId);

            Assert.Equal(userId, command.UserId);
        }
    }
}

using System;
using Wilcommerce.Auth.Commands.User;
using Xunit;

namespace Wilcommerce.Auth.Test.Commands.User
{
    public class EnableUserCommandTest
    {
        [Fact]
        public void EnableUserCommand_Ctor_Should_Set_Arguments_Correctly()
        {
            Guid userId = Guid.NewGuid();
            var command = new EnableUserCommand(userId);

            Assert.Equal(userId, command.UserId);
        }
    }
}

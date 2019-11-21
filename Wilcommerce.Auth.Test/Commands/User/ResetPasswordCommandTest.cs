using System;
using Wilcommerce.Auth.Commands.User;
using Xunit;

namespace Wilcommerce.Auth.Test.Commands.User
{
    public class ResetPasswordCommandTest
    {
        [Fact]
        public void ResetPasswordCommand_Ctor_Should_Set_Arguments_Correctly()
        {
            Guid userId = Guid.NewGuid();
            string resetToken = Guid.NewGuid().ToString(); 
            string newPassword = "password";

            var command = new ResetPasswordCommand(userId, resetToken, newPassword);

            Assert.Equal(userId, command.UserId);
            Assert.Equal(resetToken, command.ResetToken);
            Assert.Equal(newPassword, command.NewPassword);
        }
    }
}

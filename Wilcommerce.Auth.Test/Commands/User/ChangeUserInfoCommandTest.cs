using System;
using Wilcommerce.Auth.Commands.User;
using Wilcommerce.Core.Common.Models;
using Xunit;

namespace Wilcommerce.Auth.Test.Commands.User
{
    public class ChangeUserInfoCommandTest
    {
        [Fact]
        public void ChangeUserInfoCommand_Ctor_Should_Set_Arguments_Correctly()
        {
            Guid userId = Guid.NewGuid();
            string name = "Name";
            Image profileImage = new Image
            {
                MimeType = "image/png",
                Data = new byte[0]
            };

            var command = new ChangeUserInfoCommand(userId, name, profileImage);

            Assert.Equal(userId, command.UserId);
            Assert.Equal(name, command.Name);
            Assert.Equal(profileImage.MimeType, command.ProfileImage.MimeType);
            Assert.Equal(profileImage.Data, command.ProfileImage.Data);
        }
    }
}

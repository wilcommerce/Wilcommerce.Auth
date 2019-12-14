using System;
using Wilcommerce.Auth.Events.User;
using Wilcommerce.Core.Common.Models;
using Xunit;

namespace Wilcommerce.Auth.Test.Events.User
{
    public class UserInfoChangedEventTest
    {
        [Fact]
        public void UserInfoChangedEvent_Ctor_Should_Set_Arguments_Correctly()
        {
            Guid userId = Guid.NewGuid();
            string name = "admin";
            Image profileImage = new Image();

            var @event = new UserInfoChangedEvent(userId, name, profileImage);

            Assert.Equal(userId, @event.UserId);
            Assert.Equal(name, @event.Name);
            Assert.Equal(profileImage, @event.ProfileImage);

            Assert.Equal(userId, @event.AggregateId);
            Assert.Equal(typeof(Auth.Models.User), @event.AggregateType);
        }
    }
}

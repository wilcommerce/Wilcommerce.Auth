using System;
using Wilcommerce.Auth.Events.User;
using Xunit;

namespace Wilcommerce.Auth.Test.Events.User
{
    public class UserSignedInEventTest
    {
        [Fact]
        public void UserSignedInEvent_Ctor_Should_Set_Arguments_Correctly()
        {
            Guid userId = Guid.NewGuid();
            string username = "admin@admin.com";

            var @event = new UserSignedInEvent(userId, username);

            Assert.Equal(username, @event.Username);

            Assert.Equal(userId, @event.AggregateId);
            Assert.Equal(typeof(Auth.Models.User), @event.AggregateType);
        }
    }
}

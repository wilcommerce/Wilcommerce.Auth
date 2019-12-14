using System;
using Wilcommerce.Auth.Events.User;
using Xunit;

namespace Wilcommerce.Auth.Test.Events.User
{
    public class UserPasswordResetEventTest
    {
        [Fact]
        public void UserPasswordResetEvent_Ctor_Should_Set_Arguments_Correctly()
        {
            Guid userId = Guid.NewGuid();

            var @event = new UserPasswordResetEvent(userId);

            Assert.Equal(userId, @event.UserId);

            Assert.Equal(userId, @event.AggregateId);
            Assert.Equal(typeof(Auth.Models.User), @event.AggregateType);
        }
    }
}

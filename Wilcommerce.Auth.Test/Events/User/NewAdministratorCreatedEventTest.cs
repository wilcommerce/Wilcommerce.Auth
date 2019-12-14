using System;
using Wilcommerce.Auth.Events.User;
using Xunit;

namespace Wilcommerce.Auth.Test.Events.User
{
    public class NewAdministratorCreatedEventTest
    {
        [Fact]
        public void NewAdministratorCreatedEvent_Ctor_Should_Set_Arguments_Correctly()
        {
            Guid administratorId = Guid.NewGuid();
            string name = "admin";
            string email = "admin@admin.com";

            var @event = new NewAdministratorCreatedEvent(administratorId, name, email);

            Assert.Equal(administratorId, @event.AdministratorId);
            Assert.Equal(name, @event.Name);
            Assert.Equal(email, @event.Email);

            Assert.Equal(administratorId, @event.AggregateId);
            Assert.Equal(typeof(Auth.Models.User), @event.AggregateType);
        }
    }
}

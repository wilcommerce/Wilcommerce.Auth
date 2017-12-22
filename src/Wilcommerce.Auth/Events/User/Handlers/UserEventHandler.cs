using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using Wilcommerce.Auth.Services.Interfaces;
using Wilcommerce.Core.Common.Domain.ReadModels;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Auth.Events.User.Handlers
{
    public class UserEventHandler : 
        IHandleEvent<UserSignedInEvent>,
        IHandleEvent<PasswordRecoveryRequestedEvent>,
        IHandleEvent<PasswordRecoveryValidatedEvent>
    {
        public IEventStore EventStore { get; }

        public IIdentityFactory IdentityFactory { get; }

        public ICommonDatabase CommonDatabase { get; }

        public HttpContext Context { get; }

        public UserEventHandler(IEventStore eventStore, IIdentityFactory identityFactory, ICommonDatabase commonDatabase, HttpContext httpContext)
        {
            EventStore = eventStore;
            IdentityFactory = identityFactory;
            CommonDatabase = commonDatabase;
            Context = httpContext;
        }

        public void Handle(UserSignedInEvent @event)
        {
            try
            {
                EventStore.Save(@event);
            }
            catch 
            {
                throw;
            }
        }

        public void Handle(PasswordRecoveryRequestedEvent @event)
        {
            try
            {
                EventStore.Save(@event);
            }
            catch
            {
                throw;
            }
        }

        public void Handle(PasswordRecoveryValidatedEvent @event)
        {
            try
            {
                EventStore.Save(@event);

                var user = CommonDatabase.Users
                    .FirstOrDefault(u => u.Id == @event.AggregateId);

                if (user == null)
                {
                    throw new Exception("User not found");
                }

                var principal = IdentityFactory.CreateIdentity(user);
                Context.SignInAsync(AuthenticationDefaults.AuthenticationScheme, principal);
            }
            catch
            {
                throw;
            }
        }
    }
}

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using Wilcommerce.Auth.Services.Interfaces;
using Wilcommerce.Core.Common.Domain.ReadModels;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Auth.Events.User.Handlers
{
    /// <summary>
    /// Handles all the events related to the user
    /// </summary>
    public class UserEventHandler : 
        IHandleEvent<UserSignedInEvent>,
        IHandleEvent<PasswordRecoveryRequestedEvent>,
        IHandleEvent<PasswordRecoveryValidatedEvent>
    {
        /// <summary>
        /// Get the event store
        /// </summary>
        public IEventStore EventStore { get; }

        /// <summary>
        /// Get the identity factory
        /// </summary>
        public IIdentityFactory IdentityFactory { get; }

        /// <summary>
        /// Get the database of the common context
        /// </summary>
        public ICommonDatabase CommonDatabase { get; }

        /// <summary>
        /// Get the http context
        /// </summary>
        public HttpContext Context { get; }

        /// <summary>
        /// Construct the event handler
        /// </summary>
        /// <param name="eventStore">The event store instance</param>
        /// <param name="identityFactory">The identity factory instance</param>
        /// <param name="commonDatabase">The common database instance</param>
        /// <param name="httpContextAccessor">The http context accessor instance</param>
        public UserEventHandler(IEventStore eventStore, IIdentityFactory identityFactory, ICommonDatabase commonDatabase, IHttpContextAccessor httpContextAccessor)
        {
            EventStore = eventStore;
            IdentityFactory = identityFactory;
            CommonDatabase = commonDatabase;
            Context = httpContextAccessor.HttpContext;
        }

        /// <see cref="IHandleEvent{TEvent}.Handle(TEvent)"/>
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

        /// <see cref="IHandleEvent{TEvent}.Handle(TEvent)"/>
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

        /// <see cref="IHandleEvent{TEvent}.Handle(TEvent)"/>
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

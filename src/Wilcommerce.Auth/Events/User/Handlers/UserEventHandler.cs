using System;
using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Auth.Events.User.Handlers
{
    /// <summary>
    /// Handles all the events related to the user
    /// </summary>
    public class UserEventHandler : 
        IHandleEvent<UserSignedInEvent>,
        IHandleEvent<NewAdministratorCreatedEvent>,
        IHandleEvent<UserEnabledEvent>,
        IHandleEvent<UserDisabledEvent>,
        IHandleEvent<UserInfoChangedEvent>,
        IHandleEvent<UserPasswordResetEvent>
    {
        /// <summary>
        /// Get the event store
        /// </summary>
        public IEventStore EventStore { get; }

        /// <summary>
        /// Construct the event handler
        /// </summary>
        /// <param name="eventStore">The event store instance</param>
        public UserEventHandler(IEventStore eventStore)
        {
            EventStore = eventStore ?? throw new ArgumentNullException(nameof(eventStore));
        }

        /// <summary>
        /// <see cref="IHandleEvent{TEvent}"/>
        /// </summary>
        /// <param name="event"></param>
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

        /// <summary>
        /// <see cref="IHandleEvent{TEvent}"/>
        /// </summary>
        /// <param name="event"></param>
        public void Handle(NewAdministratorCreatedEvent @event)
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

        /// <summary>
        /// <see cref="IHandleEvent{TEvent}"/>
        /// </summary>
        /// <param name="event"></param>
        public void Handle(UserDisabledEvent @event)
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

        /// <summary>
        /// <see cref="IHandleEvent{TEvent}"/>
        /// </summary>
        /// <param name="event"></param>
        public void Handle(UserEnabledEvent @event)
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

        /// <summary>
        /// <see cref="IHandleEvent{TEvent}"/>
        /// </summary>
        /// <param name="event"></param>
        public void Handle(UserInfoChangedEvent @event)
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

        /// <summary>
        /// <see cref="IHandleEvent{TEvent}"/>
        /// </summary>
        /// <param name="event"></param>
        public void Handle(UserPasswordResetEvent @event)
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
    }
}

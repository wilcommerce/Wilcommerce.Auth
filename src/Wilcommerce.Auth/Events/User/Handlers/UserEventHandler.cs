using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Auth.Events.User.Handlers
{
    public class UserEventHandler : 
        IHandleEvent<UserSignedInEvent>,
        IHandleEvent<PasswordRecoveryRequestedEvent>
    {
        public IEventStore EventStore { get; }

        public UserEventHandler(IEventStore eventStore)
        {
            EventStore = eventStore;
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
    }
}

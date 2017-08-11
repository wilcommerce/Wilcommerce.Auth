using Wilcommerce.Core.Infrastructure;

namespace Wilcommerce.Auth.Events.User.Handlers
{
    public class UserEventHandler : IHandleEvent<UserSignedInEvent>
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
                
            }
        }
    }
}

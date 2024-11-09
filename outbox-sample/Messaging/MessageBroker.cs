using outbox_sample.Messaging.Abstraction;
using outbox_sample.Outbox.Abstraction;

namespace outbox_sample.Messaging;

public class MessageBroker(IOutbox outbox) : IMessageBroker
{
    public void Publish(IEvent @event)
    {
        outbox.AddMessage(@event);
    }
}
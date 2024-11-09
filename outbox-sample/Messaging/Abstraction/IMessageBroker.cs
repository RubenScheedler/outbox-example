namespace outbox_sample.Messaging.Abstraction;

public interface IMessageBroker
{
    void Publish(IEvent @event);
}
namespace outbox_sample.Outbox.Abstraction;

public interface IOutbox
{
    void AddMessage(object message);
}
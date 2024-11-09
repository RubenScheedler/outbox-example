using System.Text.Json;
using outbox_sample.Database;
using outbox_sample.Database.Entities;
using outbox_sample.Outbox.Abstraction;

namespace outbox_sample.Outbox;

public class Outbox(AppDbContext dbContext) : IOutbox
{
    public void AddMessage(object message)
    {
        var content = JsonSerializer.Serialize(message);
        var outboxMessage = new OutboxMessage(Guid.NewGuid(), message.GetType().FullName!, content, DateTimeOffset.UtcNow);

        dbContext.OutboxMessages.Add(outboxMessage);

        dbContext.SaveChanges();
    }
}
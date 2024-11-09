namespace outbox_sample.Database.Entities;

public class OutboxMessage
{
    public Guid Id { get; init; }
    public string MessageType { get; init; }
    public string Content { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset? ProcessedAt { get; set; }
    public string? Error { get; set; }

    public OutboxMessage(Guid id, string messageType, string content, DateTimeOffset createdAt)
    {
        Id = id;
        MessageType = messageType;
        Content = content;
        CreatedAt = createdAt;
    }
}
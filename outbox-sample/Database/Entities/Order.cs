namespace outbox_sample.Database.Entities;

public class Order
{
    public Guid Id { get; init; }
    public DateTimeOffset PlacedAt { get; init; }
    public Guid CustomerId { get; set; }
    public Guid ProductId { get; init; }
}
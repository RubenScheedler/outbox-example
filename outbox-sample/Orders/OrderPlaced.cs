using outbox_sample.Messaging.Abstraction;

namespace outbox_sample.Orders;

public record OrderPlaced(Guid Id, Guid CustomerId, Guid ProductId, DateTimeOffset PlacedAt) : IEvent;
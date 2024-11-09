using outbox_sample.Database;
using outbox_sample.Database.Entities;
using outbox_sample.Messaging.Abstraction;
using outbox_sample.Orders.Abstraction;

namespace outbox_sample.Orders;

public class PlaceOrderHandler(AppDbContext dbContext, IMessageBroker messageBroker) : IPlaceOrderHandler
{
    public void Handle(PlaceOrder command)
    {
        var order = new Order
        {
            Id = Guid.NewGuid(),
            PlacedAt = DateTimeOffset.UtcNow,
            CustomerId = command.CustomerId,
            ProductId = command.ProductId
        };

        dbContext.Orders.Add(order);

        messageBroker.Publish(new OrderPlaced(order.Id, order.CustomerId, order.ProductId, order.PlacedAt));
        
        dbContext.SaveChanges();
    }
}
namespace outbox_sample.Orders.Abstraction;

public interface IPlaceOrderHandler
{
    void Handle(PlaceOrder command);
}
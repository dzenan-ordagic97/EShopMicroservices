namespace Ordering.Application.Orders.EventHandlers;

public class OrderCreatedEventHandler : INotificationHandler<OrderCreatedEvent>
{
    public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

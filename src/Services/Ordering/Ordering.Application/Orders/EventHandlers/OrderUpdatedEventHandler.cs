namespace Ordering.Application.Orders.EventHandlers;

public class OrderUpdatedEventHandler : INotificationHandler<OrderUpdatedEvent>
{
    public async Task Handle(OrderUpdatedEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

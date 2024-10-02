namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer;

public class GetOrdersByCustomerHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersByCustomerQuery, GetOrdersByCustomerResult>
{
    public async Task<GetOrdersByCustomerResult> Handle(GetOrdersByCustomerQuery request, CancellationToken cancellationToken)
    {
        var ordersByCustomer = await dbContext.Orders.Include(o => o.OrderItems)
                    .AsNoTracking()
                    .Where(o => o.CustomerId == CustomerId.Of(request.CustomerId))
                    .OrderBy(o => o.OrderName)
                    .ToListAsync(cancellationToken);

         var ordersToDto = OrderExtensions.ToOrderDtoList(ordersByCustomer);

        return new GetOrdersByCustomerResult(ordersToDto);
    }
}

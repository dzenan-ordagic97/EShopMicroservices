namespace Ordering.Application.Orders.Queries.GetOrders;

public class GetOrdersHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersQuery, GetOrdersResult>
{
    public async Task<GetOrdersResult> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var pageSize = request.PaginationRequest.PageSize;
        var pageIndex = request.PaginationRequest.PageIndex;

        var totalCount = await dbContext.Orders.LongCountAsync(cancellationToken);

        var orders = await dbContext.Orders.Include(o => o.OrderItems)
                    .AsNoTracking()
                    .OrderBy(o => o.OrderName)
                    .Skip(pageSize * pageIndex)
                    .Take(pageSize)
                    .Take(request.PaginationRequest.PageSize)
                    .ToListAsync(cancellationToken);


        return new GetOrdersResult(new PaginatedResult<OrderDto>(pageIndex, pageSize, totalCount, orders.ToOrderDtoList()));
    }
}

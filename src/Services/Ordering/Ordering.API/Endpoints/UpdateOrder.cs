namespace Ordering.API.Endpoints;

public record UpdateOrderRequest(OrderDto Order);
public record UpdateOrderResponse(bool IsSuccess);

public class UpdateOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
         app.MapPut("/orders/{id}", async(UpdateOrderRequest request, ISender sender) =>
        {
            var command = request.Adapt<UpdateOrderRequest>();

            var result = await sender.Send(command);

            var response = result.Adapt<UpdateOrderResponse>();

            if (response.IsSuccess)
                return Results.NoContent();
            return Results.BadRequest();
        })
        .WithName("UpdateOrders")
        .Produces(StatusCodes.Status204NoContent)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Update a single order")
        .WithDescription("Update Order");
    }
}

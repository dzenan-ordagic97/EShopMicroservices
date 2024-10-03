namespace Ordering.API.Endpoints;

public record DeleteOrderResponse(bool IsSuccess);

public class DeleteOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/orders/{id}", async ([FromRoute] Guid id, ISender sender) =>
        {
          var result = await sender.Send(new DeleteOrderCommand(id));

          var response = result.Adapt<DeleteOrderResponse>();

          if (response.IsSuccess)
              return Results.NoContent();
          return Results.BadRequest();
        })
      .WithName("DeleteOrders")
      .Produces(StatusCodes.Status204NoContent)
      .ProducesProblem(StatusCodes.Status400BadRequest)
      .WithSummary("Deletes a single order")
      .WithDescription("Delete Order");
    }
}

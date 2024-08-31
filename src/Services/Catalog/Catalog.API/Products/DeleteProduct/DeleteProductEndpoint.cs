namespace Catalog.API.Products.DeleteProduct;
public class DeleteProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{id}", async ([FromRoute] Guid id, ISender sender) =>
       {
           var command = new DeleteProductCommand(id);

           var result = await sender.Send(command);

           if (result == Unit.Value)
               return Results.NoContent();

           return Results.BadRequest();
       })
       .WithName("DeleteProduct")
       .Produces(StatusCodes.Status204NoContent)
       .ProducesProblem(StatusCodes.Status400BadRequest)
       .WithSummary("Deletes a single product")
       .WithDescription("Delete Product");
    }
}

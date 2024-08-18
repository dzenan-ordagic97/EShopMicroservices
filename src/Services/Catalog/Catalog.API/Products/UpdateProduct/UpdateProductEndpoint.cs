using Catalog.API.Products.CreateProduct;

namespace Catalog.API.Products.UpdateProduct;

public class UpdateProductEndpoint : ICarterModule
{
    public record UpdateProductRequest(string Name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<Unit>;

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products/{id}", async([FromRoute] Guid id, [FromBody] UpdateProductRequest request, ISender sender) =>
        {
            var command = request.Adapt<UpdateProductCommand>();

            var result = await sender.Send(command);

            if (result == Unit.Value)
                return Results.NoContent();

            return Results.BadRequest();
        })
        .WithName("UpdateProduct")
        .Produces(StatusCodes.Status204NoContent)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Update a single product")
        .WithDescription("Update Product");
    }
}

namespace Catalog.API.Products.GetProductByCategory;

//public record GetProductByCategoryRequest(string categoryName);
public record GetProductByCategoryResponse(IEnumerable<Product> Products);

public class GetProductsByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}", async ([FromRoute] string category, ISender sender) =>
        {
            //var queryAdapted = request.Adapt<GetProductsByCategoryQuery>();

            var result = await sender.Send(new GetProductsByCategoryQuery(category));

            if (result == null)
                return Results.NotFound();

            var response = result.Adapt<GetProductByCategoryResponse>();

            return Results.Ok(response);
        })
      .WithName("GetProductsByCategory")
      .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
      .ProducesProblem(StatusCodes.Status400BadRequest)
      .WithSummary("Get single product by category name")
      .WithDescription("Get product by category");
    }
}

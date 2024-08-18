using Catalog.API.Products.GetProducts;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Products.GetProductById;

public record GetProductByIdRequest(Guid Id);
public record GetProductByIdResponse(Product Product);

public class GetProductByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id}", async ([FromRoute] Guid Id, ISender sender) =>
        {
            var result = await sender.Send(new GetProductByIdQuery(Id));

            if (result.Product == null)
                return Results.NotFound();

            var response = result.Adapt<GetProductByIdResponse>();

            return Results.Ok(response);
        })
      .WithName("GetProductsById")
      .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
      .ProducesProblem(StatusCodes.Status400BadRequest)
      .WithSummary("Get Single Product")
      .WithDescription("Get Product");
    }
}

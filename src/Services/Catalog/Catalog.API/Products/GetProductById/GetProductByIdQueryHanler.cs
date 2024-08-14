using Catalog.API.Exceptions;

namespace Catalog.API.Products.GetProductById;

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
public record GetProductByIdResult(Product Product);


internal class GetProductByIdQueryHanler(IDocumentSession documentSession) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await documentSession.LoadAsync<Product>(query.Id, cancellationToken);

        if (product is null)
        {
            throw new ProductNotFoundException();
        }

        return new GetProductByIdResult(product);
    }
}

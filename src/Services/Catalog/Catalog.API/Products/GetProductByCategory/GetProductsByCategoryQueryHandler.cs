using Catalog.API.Exceptions;

namespace Catalog.API.Products.GetProductByCategory;

public record GetProductsByCategoryQuery(string categoryName) : IQuery<GetProductsByCategoryResult>;
public record GetProductsByCategoryResult(IEnumerable<Product> Products);


internal class GetProductsByCategoryQueryHandler(IDocumentSession documentSession) : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
{
    public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
    {
        var products = await documentSession.Query<Product>().Where(x=> x.Category.Contains(query.categoryName)).ToListAsync(cancellationToken);

        return new GetProductsByCategoryResult(products);
    }
}

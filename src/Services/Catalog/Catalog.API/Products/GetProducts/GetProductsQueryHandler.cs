using Marten.Pagination;

namespace Catalog.API.Products.GetProducts;
public record GetProductsQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetProductsResult>;
public record GetProductsResult(IEnumerable<Product> Products);

internal class GetProductsQueryHandler(IDocumentSession documentSession) 
    : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var products = await documentSession.Query<Product>().ToPagedListAsync(query.PageNumber.GetValueOrDefault(), query.PageSize.GetValueOrDefault(), cancellationToken);

        return new GetProductsResult(products);
    }
}

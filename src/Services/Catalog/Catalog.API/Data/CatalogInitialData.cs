using Marten.Schema;

namespace Catalog.API.Data;
public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        await using var session = store.LightweightSession();

        if (await session.Query<Product>().AnyAsync())
            return;

        // Marten UPSERT will cater for existing records
        session.Store<Product>(GetPreconfiguredProducts());
        await session.SaveChangesAsync();
    }

    private static IEnumerable<Product> GetPreconfiguredProducts() => new List<Product>()
    {
        new Product()
        {
            Id = new Guid(),
            Name = "0191467d-b8cf-4d7b-9cc7-50e0ecfd0991",
            Description = "Description of Iphone X",
            ImageFile = "product-1.png",
            Price = 950.00M,
            Category = new List<string> { "Smartphone" }
        },
        new Product()
        {
            Id = new Guid(),
            Name = "0191467d-b8cf-4d7b-9cc7-50e0ecfd0992",
            Description = "Samsung S23 Ultra",
            ImageFile = "product-2.png",
            Price = 1000.00M,
            Category = new List<string> { "Smartphone" }
        },
        new Product()
        {
            Id = new Guid(),
            Name = "0191467d-b8cf-4d7b-9cc7-50e0ecfd0993",
            Description = "Description of Huawei PRO",
            ImageFile = "product-3.png",
            Price = 900.00M,
            Category = new List<string> { "Smartphone" }
        }
    };
}

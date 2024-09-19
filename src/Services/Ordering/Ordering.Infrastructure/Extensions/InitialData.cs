using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Extensions
{
    internal class InitialData
    {
        public static IEnumerable<Customer> Customers =>
            new List<Customer>() {
                Customer.Create(CustomerId.Of(new Guid("customer-123")), "Dzenan", "dzenan@gmail.com"),
                Customer.Create(CustomerId.Of(new Guid("customer-567")), "Osman", "osman@gmail.com")
            };

        public static IEnumerable<Product> Products =>
            new List<Product>() {
                Product.Create(ProductId.Of(new Guid("product-123")), "Product 1", 100),
                Product.Create(ProductId.Of(new Guid("product-567")), "Product 2", 200)
            };

        public static IEnumerable<Order> OrdersWithItems =>
            new List<Order>() {
                //TODO Add test data.
            };
    }
}

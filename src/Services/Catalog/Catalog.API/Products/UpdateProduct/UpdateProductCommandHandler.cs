using Catalog.API.Exceptions;

namespace Catalog.API.Products.UpdateProduct;
public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand;

internal class UpdateProductCommandHandler(IDocumentSession documentSession) : ICommandHandler<UpdateProductCommand>
{
    public async Task<Unit> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await documentSession.LoadAsync<Product>(command.Id, cancellationToken);

        if (product == null)
            throw new ProductNotFoundException();

        product.Name = command.Name;
        product.Category = command.Category;
        product.Description = command.Description;
        product.ImageFile = command.ImageFile;
        product.Price = command.Price;

        documentSession.Update(product);
        await documentSession.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}

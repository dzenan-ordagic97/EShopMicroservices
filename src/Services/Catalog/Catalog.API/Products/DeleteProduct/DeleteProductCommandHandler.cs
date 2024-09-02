namespace Catalog.API.Products.DeleteProduct;

public record DeleteProductCommand(Guid Id) : ICommand;

public class DeleteProductCommandValidator: AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(command => command.Id).NotEmpty().WithMessage("Product ID is required");
    }
}

internal class DeleteProductCommandHandler(IDocumentSession documentSession, 
    ILogger<DeleteProductCommandHandler> logger) : ICommandHandler<DeleteProductCommand>
{
    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Entered DeleteProductCommandHandler method..");

        documentSession.Delete<Product>(request.Id);
        await documentSession.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}

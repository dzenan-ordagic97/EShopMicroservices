using Basket.API.Data;
using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace Basket.API.Basket.CheckoutBasket;

public record CheckoutBasketCommand(BasketCheckoutDto BasketCheckoutDto) : ICommand<CheckoutBasketResult>;
public record CheckoutBasketResult(bool IsSuccess);

public class CheckoutBasketCommandValidator : AbstractValidator<CheckoutBasketCommand>
{
    public CheckoutBasketCommandValidator()
    {
        RuleFor(x => x.BasketCheckoutDto).NotNull().WithMessage("BasketCheckoutDto can not be null");
        RuleFor(x => x.BasketCheckoutDto.UserName).NotNull().WithMessage("Username is required");
    }
}

public class CheckoutBasketHandler(IBasketRepository basketRepository, IPublishEndpoint publishEndpoint) : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
{
    public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand request, CancellationToken cancellationToken)
    {
        var basket = await basketRepository.GetBasket(request.BasketCheckoutDto.UserName, cancellationToken);
        if (basket == null) {
            return new CheckoutBasketResult(false);
        }

        var eventMessage = request.BasketCheckoutDto.Adapt<BasketCheckoutEvent>();
        eventMessage.TotalPrice = basket.TotalPrice;

        await publishEndpoint.Publish(eventMessage);

        await basketRepository.DeleteBasket(request.BasketCheckoutDto.UserName, cancellationToken);

        return new CheckoutBasketResult(true);
    }
}

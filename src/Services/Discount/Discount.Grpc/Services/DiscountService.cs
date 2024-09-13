using Discount.Grpc.Data;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Mapster;
using Discount.Grpc.Models;

namespace Discount.Grpc.Services;
public class DiscountService(DiscountContext dbContext) : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

        if (coupon is null)
            coupon = new Models.Coupon() { ProductName = "No Discount", Amount = 0, Description = "Test" };

        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Adapt<Coupon>();

        if (coupon is null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object."));

        await dbContext.Coupons.AddAsync(coupon);
        await dbContext.SaveChangesAsync();

        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

        if (coupon is null)
            throw new RpcException(new Status(StatusCode.NotFound, "Discount coupon not found."));

        dbContext.Coupons.Remove(coupon);
        await dbContext.SaveChangesAsync();

        return new DeleteDiscountResponse() { Success = true };
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Adapt<Coupon>();
        if (coupon is null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object."));

        dbContext.Coupons.Update(coupon);
        await dbContext.SaveChangesAsync();

        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
    }
}

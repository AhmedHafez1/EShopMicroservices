using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services
{
    public class DiscountService(DiscountContext discountContext, ILogger<DiscountService> logger) : DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await discountContext.Coupons.FirstOrDefaultAsync(coupon => coupon.ProductName == request.ProductName);

            if (coupon == null)
                coupon = new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount" };

            logger.LogInformation($"Discount retrieved for product {request.ProductName}, amount: {coupon.Amount}");

            var couponModel = coupon.Adapt<CouponModel>();

            return couponModel;
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if (coupon == null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Coupon is not valid!"));

            await discountContext.AddAsync(coupon);
            await discountContext.SaveChangesAsync();

            logger.LogInformation($"Discount successfully created for product {coupon.ProductName}, amount: {coupon.Amount}");

            var couponModel = coupon.Adapt<CouponModel>();

            return couponModel;
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if (coupon == null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Coupon is not valid!"));

            discountContext.Update(coupon);
            await discountContext.SaveChangesAsync();

            logger.LogInformation($"Discount successfully updated for product {coupon.ProductName}, amount: {coupon.Amount}");

            var couponModel = coupon.Adapt<CouponModel>();

            return couponModel;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = await discountContext.Coupons.FirstOrDefaultAsync(p => p.ProductName == request.ProductName);

            if (coupon == null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "No Coupon for given product!"));

            discountContext.Remove(coupon);
            await discountContext.SaveChangesAsync();

            logger.LogInformation($"Discount successfully deleted for product {coupon.ProductName}");

            return new DeleteDiscountResponse { Success = true };
        }
    }
}

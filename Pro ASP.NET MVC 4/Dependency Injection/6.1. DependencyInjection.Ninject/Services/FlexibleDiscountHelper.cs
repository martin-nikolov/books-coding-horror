namespace DependencyInjection.Ninject.Services
{
    using DependencyInjection.Ninject.Services.Contracts;

    public class FlexibleDiscountHelper : IDiscountHelper
    {
        public decimal ApplyDiscount(decimal totalPrice)
        {
            decimal discount = totalPrice > 100 ? 70 : 25;

            return totalPrice - (discount / 100m * totalPrice);
        }
    }
}
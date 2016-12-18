namespace DependencyInjection.Ninject.Services
{
    using DependencyInjection.Ninject.Services.Contracts;

    public class DefaultDiscountHelper : IDiscountHelper
    {
        private readonly decimal discountPercent;

        public DefaultDiscountHelper(decimal discountPercent)
        {
            this.discountPercent = discountPercent;
        }

        public decimal ApplyDiscount(decimal totalPrice)
        {
            return totalPrice - (this.discountPercent / 100m * totalPrice);
        }
    }
}
namespace DependencyInjection.Ninject.Services
{
    public class DefaultDiscountHelper : IDiscountHelper
    {
        private readonly decimal discountSize;

        public DefaultDiscountHelper(decimal discountSize)
        {
            this.discountSize = discountSize;
        }

        public decimal ApplyDiscount(decimal totalPrice)
        {
            return totalPrice - (this.discountSize / 100m * totalPrice);
        }
    }
}
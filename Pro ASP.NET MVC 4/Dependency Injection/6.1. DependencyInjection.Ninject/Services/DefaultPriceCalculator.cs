namespace DependencyInjection.Ninject.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using DependencyInjection.Ninject.Models;
    using DependencyInjection.Ninject.Services.Contracts;

    public class DefaultPriceCalculator : IPriceCalculator
    {
        private readonly IDiscountHelper discountHelper;

        public DefaultPriceCalculator(IDiscountHelper discountHelper)
        {
            this.discountHelper = discountHelper;
        }

        public decimal CalculateTotalPrice(IEnumerable<Product> products)
        {
            decimal totalPrice = products.Sum(p => p.Price);

            return totalPrice;
        }

        public decimal ApplyDiscount(decimal totalPrice)
        {
            var totalPriceWithDiscount = this.discountHelper.ApplyDiscount(totalPrice);

            return totalPriceWithDiscount;
        }
    }
}
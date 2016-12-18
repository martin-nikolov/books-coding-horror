namespace DependencyInjection.Ninject.Services
{
    using System;
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
            if (products == null)
            {
                throw new ArgumentException("The collection of products cannot be null.");
            }

            decimal totalPrice = products.Sum(p => p.Price);

            return totalPrice;
        }

        public decimal ApplyDiscount(decimal totalPrice)
        {
            if (totalPrice < 0)
            {
                throw new ArgumentException("totalPrice cannot be less than 0.");
            }

            var totalPriceWithDiscount = this.discountHelper.ApplyDiscount(totalPrice);

            return totalPriceWithDiscount;
        }
    }
}
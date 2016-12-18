namespace DependencyInjection.Ninject.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using DependencyInjection.Ninject.Models;

    public class DefaultPriceCalculator : IPriceCalculator
    {
        public decimal CalculateTotalPrice(IEnumerable<Product> products)
        {
            decimal totalPrice = products.Sum(p => p.Price);

            return totalPrice;
        }
    }
}
namespace DependencyInjection.Ninject.Services.Contracts
{
    using System.Collections.Generic;
    using DependencyInjection.Ninject.Models;

    public interface IPriceCalculator
    {
        decimal CalculateTotalPrice(IEnumerable<Product> products);

        decimal ApplyDiscount(decimal totalPrice);
    }
}
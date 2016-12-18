namespace DependencyInjection.Ninject.Services
{
    using System.Collections.Generic;
    using DependencyInjection.Ninject.Models;

    public interface IPriceCalculator
    {
        decimal CalculateTotalPrice(IEnumerable<Product> products);
    }
}
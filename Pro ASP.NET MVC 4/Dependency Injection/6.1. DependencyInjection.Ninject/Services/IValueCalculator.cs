namespace DependencyInjection.Ninject.Services
{
    using System.Collections.Generic;
    using DependencyInjection.Ninject.Models;

    public interface IValueCalculator
    {
        decimal CalculateTotalPrice(IEnumerable<Product> products);
    }
}
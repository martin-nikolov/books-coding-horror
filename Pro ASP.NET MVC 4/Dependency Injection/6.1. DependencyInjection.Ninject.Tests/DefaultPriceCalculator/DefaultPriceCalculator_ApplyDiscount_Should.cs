namespace DependencyInjection.Ninject.Tests.DefaultPriceCalculator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DependencyInjection.Ninject.Models;
    using DependencyInjection.Ninject.Services;
    using DependencyInjection.Ninject.Services.Contracts;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

    [TestClass]
    public class DefaultPriceCalculator_ApplyDiscount_Should
    {
        [TestMethod]
        public void Returns_SamePriceWithoutDiscount()
        {
            // Arrange
            var mockedDiscountHelper = new Mock<IDiscountHelper>();
            mockedDiscountHelper.Setup(m => m.ApplyDiscount(It.IsAny<decimal>())).Returns<decimal>(total => total);

            var defaultPriceCalculator = new DefaultPriceCalculator(mockedDiscountHelper.Object);
            var products = this.GetProducts().ToArray();

            // Act
            var totalPrice = defaultPriceCalculator.CalculateTotalPrice(products);
            var totalPriceWithDiscount = defaultPriceCalculator.ApplyDiscount(totalPrice);

            // Assert
            var totalPriceOfProducts = products.Sum(p => p.Price);

            Assert.AreEqual(totalPriceOfProducts, totalPrice);
            Assert.AreEqual(totalPriceOfProducts, totalPriceWithDiscount);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ThrowsException_When_TotalPriceIsNegativeNumber()
        {
            // Arrange
            var mockedDiscountHelper = new Mock<IDiscountHelper>();
            mockedDiscountHelper.Setup(m => m.ApplyDiscount(It.IsAny<decimal>())).Returns<decimal>(total => total);

            var defaultPriceCalculator = new DefaultPriceCalculator(mockedDiscountHelper.Object);

            // Act
            defaultPriceCalculator.ApplyDiscount(-1);
        }

        private IEnumerable<Product> GetProducts()
        {
            var products = new List<Product>()
            {
                new Product("Product 1", 100),
                new Product("Product 2", 200),
                new Product("Product 3", 300)
            };

            return products;
        }
    }
}
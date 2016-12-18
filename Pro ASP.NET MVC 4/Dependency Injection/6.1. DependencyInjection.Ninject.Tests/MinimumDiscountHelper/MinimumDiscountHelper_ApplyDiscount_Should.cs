namespace DependencyInjection.Ninject.Tests.MinimumDiscountHelper
{
    using System;
    using DependencyInjection.Ninject.Services;
    using DependencyInjection.Ninject.Services.Contracts;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// * If the total is greater than $100, the discount will be 10 percent.
    /// * If the total is between $10 and $100 inclusive, the discount will be $5.
    /// * No discount will be applied on totals less than $10.
    /// * An <see cref="ArgumentException" /> will be thrown for negative totals.
    /// </summary>
    [TestClass]
    public class MinimumDiscountHelper_ApplyDiscount_Should
    {
        [TestMethod]
        public void Returns_PriceWithDiscountOf10Percent_When_PriceIsAbove100()
        {
            // Arrange
            var discountHelper = this.GetDiscountHelperInstance();
            var totalPrice = 200m;
            var discountPercent = 10m;

            // Act
            var totalPriceWithDiscount = discountHelper.ApplyDiscount(totalPrice);

            // Assert
            Assert.AreEqual(totalPrice * ((100 - discountPercent) / 100), totalPriceWithDiscount);
        }

        [TestMethod]
        public void Returns_PriceWithDiscountOf5Dollars_When_PriceIsBetween10And100()
        {
            // Arrange
            var discountHelper = this.GetDiscountHelperInstance();

            // Act
            var tenDollarDiscount = discountHelper.ApplyDiscount(10);
            var fiftyDollarDiscount = discountHelper.ApplyDiscount(50);
            var hundredDollarDiscount = discountHelper.ApplyDiscount(100);

            // Assert
            Assert.AreEqual(5, tenDollarDiscount, "$10 discount is wrong.");
            Assert.AreEqual(45, fiftyDollarDiscount, "$50 discount is wrong.");
            Assert.AreEqual(95, hundredDollarDiscount, "$100 discount is wrong.");
        }

        [TestMethod]
        public void Returns_SamePriceWithoutDiscount_When_PriceIsLessThan10()
        {
            // Arrange
            var discountHelper = this.GetDiscountHelperInstance();

            // Act
            var zeroDollarDiscount = discountHelper.ApplyDiscount(0);
            var fiveDollarDiscount = discountHelper.ApplyDiscount(5);

            // Assert
            Assert.AreEqual(0, zeroDollarDiscount, "0 discount is wrong.");
            Assert.AreEqual(5, fiveDollarDiscount, "5 discount is wrong.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ThrowsException_When_PriceIsNegativeNumber()
        {
            // Arrange
            var discountHelper = this.GetDiscountHelperInstance();

            // Act
            discountHelper.ApplyDiscount(-1);
        }

        private IDiscountHelper GetDiscountHelperInstance()
        {
            var instance = new MinimumDiscountHelper();

            return instance;
        }
    }
}
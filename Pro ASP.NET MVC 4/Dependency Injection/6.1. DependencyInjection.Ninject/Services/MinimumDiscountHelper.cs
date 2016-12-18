namespace DependencyInjection.Ninject.Services
{
    using System;
    using DependencyInjection.Ninject.Services.Contracts;

    /// <summary>
    /// * If the total is greater than $100, the discount will be 10 percent.
    /// * If the total is between $10 and $100 inclusive, the discount will be $5.
    /// * No discount will be applied on totals less than $10.
    /// * An <see cref="ArgumentException" /> will be thrown for negative totals.
    /// </summary>
    public class MinimumDiscountHelper : IDiscountHelper
    {
        public decimal ApplyDiscount(decimal totalPrice)
        {
            if (totalPrice < 0)
            {
                throw new ArgumentException("totalPrice cannot be less than 0.");
            }


            if (totalPrice > 100)
            {
                return totalPrice * .9m;
            }
            else if (totalPrice >= 10 && totalPrice <= 100)
            {
                return totalPrice - 5;
            }
            else
            {
                return totalPrice;
            }
        }
    }
}
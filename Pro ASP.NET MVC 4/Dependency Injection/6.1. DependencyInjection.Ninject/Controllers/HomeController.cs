namespace DependencyInjection.Ninject.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using DependencyInjection.Ninject.Infrastructure;
    using DependencyInjection.Ninject.Models;
    using DependencyInjection.Ninject.Services;

    public class HomeController : Controller
    {
        private readonly IPriceCalculator priceCalculator;

        public HomeController(IPriceCalculator priceCalculator)
        {
            this.priceCalculator = priceCalculator;       
        }

        public ActionResult Index()
        {
            var products = new List<Product>()
            {
                new Product("Product 1", 100),
                new Product("Product 2", 200),
                new Product("Product 3", 300)
            };

            IDiscountHelper discountHelper = ObjectFactory.GetInstance<IDiscountHelper>();

            decimal totalPrice = this.priceCalculator.CalculateTotalPrice(products);
            decimal totalPriceWithDiscount = discountHelper.ApplyDiscount(totalPrice);

            this.ViewBag.TotalPrice = totalPrice;
            this.ViewBag.TotalPriceWithDiscount = totalPriceWithDiscount;

            return this.View();
        }
    }
}
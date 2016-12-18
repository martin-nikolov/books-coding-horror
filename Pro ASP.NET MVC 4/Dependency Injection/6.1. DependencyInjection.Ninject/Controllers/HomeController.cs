namespace DependencyInjection.Ninject.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using DependencyInjection.Ninject.Models;
    using DependencyInjection.Ninject.Services;

    public class HomeController : Controller
    {
        private readonly IValueCalculator valueCalculator;

        public HomeController(IValueCalculator valueCalculator)
        {
            this.valueCalculator = valueCalculator;       
        }

        public ActionResult Index()
        {
            var products = new List<Product>()
            {
                new Product("Product 1", 100),
                new Product("Product 2", 200),
                new Product("Product 3", 300)
            };

            var totalPrice = this.valueCalculator.CalculateTotalPrice(products);
            this.ViewBag.TotalPrice = totalPrice;

            return this.View();
        }
    }
}
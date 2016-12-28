namespace Controllers.Extensibility.Controllers
{
    using System.Web.Mvc;
    using global::Controllers.Extensibility.Models;

    public class ProductController : Controller
    {
        public ActionResult Index()
        {
            return this.View("Result", new Result() { ControllerName = "Product", ActionName = "Index" });
        }

        public ActionResult List()
        {
            return this.View("Result", new Result() { ControllerName = "Product", ActionName = "List" });
        }
    }
}
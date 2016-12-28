namespace Controllers.Extensibility.Controllers
{
    using System.Web.Mvc;
    using global::Controllers.Extensibility.Models;

    public class CustomerController : Controller
    {
        public ActionResult Index()
        {
            return this.View("Result", new Result() { ControllerName = "Customer", ActionName = "Index" });
        }

        public ActionResult List()
        {
            return this.View("Result", new Result() { ControllerName = "Customer", ActionName = "List" });
        }
    }
}
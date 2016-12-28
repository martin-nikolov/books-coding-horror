namespace CustomRouteHandler.Example.Controllers
{
    using System.Web.Mvc;

    public class ErrorController : Controller
    {
        public ActionResult Index(string aspxerrorpath)
        {
            if (string.IsNullOrWhiteSpace(aspxerrorpath))
            {
                return this.RedirectToAction("Index", "Home");
            }

            this.ViewBag.Title = "Error occurs!";

            return this.View("Index");
        }

        public ActionResult NotFound(string aspxerrorpath)
        {
            if (string.IsNullOrWhiteSpace(aspxerrorpath))
            {
                return this.RedirectToAction("Index", "Home");
            }

            this.ViewBag.Title = "Error - not found!";

            return this.View("NotFound");
        }

        public ActionResult Error()
        {
            return this.View();
        }
    }
}
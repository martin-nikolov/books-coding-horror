namespace CustomRouteHandler.Example.Controllers
{
    using System.Web.Mvc;
    using CustomRouteHandler.Example.Infrastructure;

    public class HomeController : Controller
    {
        [CustomFilter]
        [OutputCache(Duration = 5, VaryByParam = "*")]
        public ActionResult Index(string id)
        {
            return this.View();
        }
    }
}
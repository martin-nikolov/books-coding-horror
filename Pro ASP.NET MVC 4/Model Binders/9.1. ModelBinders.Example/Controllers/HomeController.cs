namespace ModelBinders.Example.Controllers
{
    using System.Web.Mvc;
    using ModelBinders.Example.Models;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Summary(Card card)
        {
            return this.View(card);
        }
    }
}
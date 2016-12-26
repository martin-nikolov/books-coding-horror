using System;
using System.Linq;
using System.Web.Mvc;

namespace WebApplication1.Admin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return this.View();
        }
    }
}
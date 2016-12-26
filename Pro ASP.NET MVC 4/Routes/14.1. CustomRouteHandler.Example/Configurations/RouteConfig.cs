namespace CustomRouteHandler.Example.Configurations
{
    using System.Web.Mvc;
    using System.Web.Routing;
    using CustomRouteHandler.Example.Infrastructure;

    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.Add(new Route("SayHello", new CustomRouteHandler()));

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }
    }
}
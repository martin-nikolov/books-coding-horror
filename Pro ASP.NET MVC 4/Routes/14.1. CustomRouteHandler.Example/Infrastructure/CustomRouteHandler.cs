namespace CustomRouteHandler.Example.Infrastructure
{
    using System.Web;
    using System.Web.Routing;

    public class CustomRouteHandler : IRouteHandler
    {
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new CustomHttpHandler();
        }
    }
}
namespace CustomRouteHandler.Example.Infrastructure
{
    using System.Web;

    public class CustomHttpHandler : IHttpHandler
    {
        public bool IsReusable { get; } = true;

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Write("Hello");
        }
    }
}
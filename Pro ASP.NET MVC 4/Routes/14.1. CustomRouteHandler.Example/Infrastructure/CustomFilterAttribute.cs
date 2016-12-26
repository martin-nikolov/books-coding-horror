namespace CustomRouteHandler.Example.Infrastructure
{
    using System;
    using System.Diagnostics;
    using System.Web.Mvc;

    [AttributeUsage(AttributeTargets.Method)]
    public class CustomFilterAttribute : ActionFilterAttribute
    {
        private Stopwatch stopWatch;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            this.stopWatch = Stopwatch.StartNew();
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            this.stopWatch.Stop();

            filterContext.Controller.TempData["ElapsedTime"] = this.stopWatch.Elapsed.Milliseconds;
        }
    }
}
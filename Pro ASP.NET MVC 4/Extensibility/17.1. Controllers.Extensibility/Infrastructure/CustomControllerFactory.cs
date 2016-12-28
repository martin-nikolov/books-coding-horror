namespace Controllers.Extensibility.Infrastructure
{
    using System;
    using System.Web.Mvc;
    using System.Web.Routing;
    using System.Web.SessionState;
    using global::Controllers.Extensibility.Controllers;

    public class CustomControllerFactory : IControllerFactory
    {
        public IController CreateController(RequestContext requestContext, string controllerName)
        {
            Type targetType = null;

            switch (controllerName)
            {
                case "Product":
                {
                    targetType = typeof(ProductController);
                    break;
                }

                case "Customer":
                {
                    targetType = typeof(CustomerController);
                    break;
                }

                default:
                {
                    // Fallback controller
                    requestContext.RouteData.Values["controller"] = "Product";
                    targetType = typeof(ProductController);
                    break;
                }
            }

            return DependencyResolver.Current.GetService(targetType) as IController;
        }

        public SessionStateBehavior GetControllerSessionBehavior(RequestContext requestContext, string controllerName)
        {
            return SessionStateBehavior.Default;
        }

        public void ReleaseController(IController controller)
        {
            var disposable = controller as IDisposable;
            disposable?.Dispose();
        }
    }
}
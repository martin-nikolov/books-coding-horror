namespace ModelBinders.Example.Binders
{
    using System.Web.Mvc;
    using ModelBinders.Example.Models;

    public class CardModelBinder : IModelBinder
    {
        private const string sessionKey = "Card";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            Card card = null;

            if (controllerContext?.HttpContext?.Session != null)
            {
                card = controllerContext.HttpContext.Session[sessionKey] as Card;

                if (card == null)
                {
                    card = new Card();
                    controllerContext.HttpContext.Session[sessionKey] = card;
                }
            }

            return card;
        }
    }
}
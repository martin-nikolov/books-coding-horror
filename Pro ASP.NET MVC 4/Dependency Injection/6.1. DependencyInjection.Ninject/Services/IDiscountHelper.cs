namespace DependencyInjection.Ninject.Services
{
    public interface IDiscountHelper
    {
        decimal ApplyDiscount(decimal totalPrice);
    }
}
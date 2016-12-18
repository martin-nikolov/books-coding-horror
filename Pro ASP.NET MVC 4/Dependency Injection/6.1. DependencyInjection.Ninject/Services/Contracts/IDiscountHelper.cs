namespace DependencyInjection.Ninject.Services.Contracts
{
    public interface IDiscountHelper
    {
        decimal ApplyDiscount(decimal totalPrice);
    }
}
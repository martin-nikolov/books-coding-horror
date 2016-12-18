namespace DependencyInjection.Ninject.Registries
{
    using DependencyInjection.Ninject.Services;
    using global::Ninject;

    public class ServiceBindingsRegister : INinjectBindingsRegister
    {
        private const int DefaultDiscountPercentage = 50;

        public void Register(IKernel kernel)
        {
            kernel.Bind<IPriceCalculator>().To<DefaultPriceCalculator>();
            kernel.Bind<IDiscountHelper>().To<DefaultDiscountHelper>().WithConstructorArgument<decimal>(ServiceBindingsRegister.DefaultDiscountPercentage);
        }
    }
}
namespace DependencyInjection.Ninject.Registries
{
    using DependencyInjection.Ninject.Services;
    using global::Ninject;

    public class ServiceBindingsRegister : INinjectBindingsRegister
    {
        public void Register(IKernel kernel)
        {
            kernel.Bind<IDiscountHelper>().To<DefaultDiscountHelper>();
            kernel.Bind<IValueCalculator>().To<DefaultValueCalculator>();
        }
    }
}
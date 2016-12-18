namespace DependencyInjection.Ninject.Registries
{
    using global::Ninject;

    public interface INinjectBindingsRegister
    {
        void Register(IKernel kernel);
    }
}
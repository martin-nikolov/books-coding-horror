namespace AsyncInitializationPattern.Infrastructure
{
    using AsyncInitializationPattern.Abstract;
    using AsyncInitializationPattern.Imp;
    using Ninject;

    public static class DependencyInjection
    {
        private static readonly IKernel kernel = new StandardKernel();

        public static void RegisterDependencies()
        {
            kernel.Bind<IMyFundamentalType>().To<MyFundamentalType>();
            kernel.Bind<IMyComposedType>().To<MyComposedType>();
            kernel.Bind<ILogger>().To<ConsoleLogger>();
        }

        public static T Get<T>()
        {
            return kernel.Get<T>();
        }
    }
}
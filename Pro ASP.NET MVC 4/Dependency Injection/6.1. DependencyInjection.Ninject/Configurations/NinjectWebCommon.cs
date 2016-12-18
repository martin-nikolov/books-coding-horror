[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(DependencyInjection.Ninject.Configurations.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(DependencyInjection.Ninject.Configurations.NinjectWebCommon), "Stop")]

namespace DependencyInjection.Ninject.Configurations
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Web;
    using DependencyInjection.Ninject.Infrastructure;
    using DependencyInjection.Ninject.Registries;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using global::Ninject;
    using global::Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            ObjectFactory.InitializeKernel(kernel);

            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            var registries = Assembly.GetExecutingAssembly()
                                     .GetExportedTypes()
                                     .Where(t => t.IsClass && typeof(INinjectBindingsRegister).IsAssignableFrom(t));

            foreach (var registry in registries)
            {
                var registryInstance = (INinjectBindingsRegister)Activator.CreateInstance(registry);
                registryInstance.Register(kernel);
            }
        }        
    }
}

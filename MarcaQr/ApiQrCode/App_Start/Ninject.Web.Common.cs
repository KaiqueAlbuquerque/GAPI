[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(ApiQrCode.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(ApiQrCode.App_Start.NinjectWebCommon), "Stop")]

namespace ApiQrCode.App_Start
{
    using System;
    using System.Web;
    using System.Web.Http;
    using Application;
    using Application.Interfaces;
    using Dominio.Interfaces.Intermediadores;
    using Dominio.Interfaces.ObjVal;
    using Dominio.Interfaces.Repositorios;
    using Dominio.Intermediadores;
    using Dominio.ObjVal;
    using Infra.Repositorios;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;
    using Ninject.Web.WebApi;

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
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                RegisterServices(kernel);
                GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);
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
            kernel.Bind<IDashboard>().To<Dashboard>();

            kernel.Bind(typeof(IAppBase<>)).To(typeof(AppBase<>));
            kernel.Bind<IAppCliente>().To<AppCliente>();
            kernel.Bind<IAppLog>().To<AppLog>();
            kernel.Bind<IAppUsuario>().To<AppUsuario>();

            kernel.Bind(typeof(IIntermediadorBase<>)).To(typeof(IntermediadorBase<>));
            kernel.Bind<IIntermediadorCliente>().To<IntermediadorCliente>();
            kernel.Bind<IIntermediadorLog>().To<IntermediadorLog>();
            kernel.Bind<IIntermediadorUsuario>().To<IntermediadorUsuario>();
            
            kernel.Bind(typeof(IRepositorioBase<>)).To(typeof(RepositorioBase<>));
            kernel.Bind<IRepositorioCliente>().To<RepositorioCliente>();
            kernel.Bind<IRepositorioLog>().To<RepositorioLog>();
            kernel.Bind<IRepositorioUsuario>().To<RepositorioUsuario>();
        }
    }
}
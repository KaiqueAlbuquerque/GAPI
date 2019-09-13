using API.App_Start;
using Infra.Repositorios;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Common.WebHost;
using Servico.Interfaces.Repositorio;
using Servico.Interfaces.Servico;
using Servico.Servico;
using System;
using System.Web;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NinjectWebCommon), "Stop")]

namespace API.App_Start
{
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            RegisterServices(kernel);
            return kernel;
        }
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind(typeof(IBaseServico<>)).To(typeof(BaseServico<>));
            kernel.Bind<ITokenServico>().To<TokenServico>();
            kernel.Bind<IEmpresaServico>().To<EmpresaServico>();
            kernel.Bind<IUsuarioServico>().To<UsuarioServico>();
            kernel.Bind<ILogsServico>().To<LogsServico>();

            kernel.Bind(typeof(IBaseRepositorio<>)).To(typeof(BaseRepositorio<>));
            kernel.Bind<ITokenRepositorio>().To<TokenRepositorio>();
            kernel.Bind<IEmpresaRepositorio>().To<EmpresaRepositorio>();
            kernel.Bind<IUsuarioRepositorio>().To<UsuarioRepositorio>();
            kernel.Bind<ILogsRepositorio>().To<LogsRepositorio>();
        }
    }
}
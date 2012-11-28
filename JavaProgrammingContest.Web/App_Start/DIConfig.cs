using System.Data.Entity;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using JavaProgrammingContest.DataAccess.Context;
using JavaProgrammingContest.Process.Compiler;
using JavaProgrammingContest.Process.Compiler.Java;
using JavaProgrammingContest.Process.Compiler.Java.Helpers;
using JavaProgrammingContest.Process.Runner;
using JavaProgrammingContest.Process.Runner.Java;
using JavaProgrammingContest.Process.Runner.Java.Helpers;

// ReSharper disable CheckNamespace

namespace JavaProgrammingContest.Web{
    public class DIConfig{
        public static void RegisterTypes(){
            var builder = new ContainerBuilder();

            RegisterControllers(builder);
            RegisterDataContext(builder);
            RegisterOther(builder);

            var container = builder.Build();

            SetApiResolver(container);
            SetMvcResolver(container);
        }

        private static void RegisterDataContext(ContainerBuilder builder){
            builder.RegisterType<JavaProgrammingContestContext>().As<IDbContext>();
        }

        private static void RegisterControllers(ContainerBuilder builder){
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterControllers(typeof (MvcApplication).Assembly);
        }

        private static void RegisterOther(ContainerBuilder builder){
            builder.RegisterType<JavaCompiler>().As<ICompiler>().PropertiesAutowired();
            builder.RegisterType<JavaCompilerProcess>().As<ICompilerProcess>();
            builder.RegisterType<JavaRunner>().As<IRunner>().PropertiesAutowired();
            builder.RegisterType<JavaRunnerProcess>().As<IRunnerProcess>();
        }

        private static void SetMvcResolver(ILifetimeScope container){
            var mvcResolver = new AutofacDependencyResolver(container);
            DependencyResolver.SetResolver(mvcResolver);
        }

        private static void SetApiResolver(ILifetimeScope container){
            var webApiResolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = webApiResolver;
        }
    }
}
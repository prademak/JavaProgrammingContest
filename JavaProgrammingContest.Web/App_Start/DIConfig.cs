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
    public static class DIConfig{
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
            builder.RegisterType<JavaProgrammingContestContext>().As<IDbContext>().SingleInstance();
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
            builder.RegisterType<SettingsReader>().As<ISettingsReader>();
            builder.RegisterType<FilePathCreator>().As<IFilePathCreator>();
        }

        private static void SetMvcResolver(ILifetimeScope container){
            DependencyResolver.SetResolver(
                new AutofacDependencyResolver(container)
            );
        }

        private static void SetApiResolver(ILifetimeScope container){
            GlobalConfiguration.Configuration.DependencyResolver =
                new AutofacWebApiDependencyResolver(container);
        }
    }
}
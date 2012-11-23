using System.Data.Entity;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using JavaProgrammingContest.DataAccess;
using JavaProgrammingContest.DataAccess.Context;
using JavaProgrammingContest.DataAccess.SQLServer;

// ReSharper disable CheckNamespace

namespace JavaProgrammingContest.Web{
    public class DIConfig{
        public static void RegisterTypes(){
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterControllers(typeof (MvcApplication).Assembly);
            builder.RegisterType<JavaProgrammingContestContext>().As<DbContext>();
            builder.RegisterGeneric(typeof (GenericRepository<>)).As(typeof (IRepository<>));

            var container = builder.Build();

            var webApiResolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = webApiResolver;

            var mvcResolver = new AutofacDependencyResolver(container);
            DependencyResolver.SetResolver(mvcResolver);
        }
    }
}
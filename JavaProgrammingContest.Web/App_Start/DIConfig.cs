using System.Data.Entity;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using JavaProgrammingContest.DataAccess.Context;

namespace JavaProgrammingContest.Web.App_Start{
    public class DIConfig{
        public static void RegisterTypes(){
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof (MvcApplication).Assembly);
            builder.RegisterType<JavaProgrammingContestContext>().As<DbContext>();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(builder.Build()));
        }
    }
}
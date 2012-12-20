using System.Web.Mvc;
using System.Web.Routing;

namespace JavaProgrammingContest.Web.App_Start{
    /// <summary>
    ///     Configures Application Routes
    /// </summary>
    public static class RouteConfig{
        /// <summary>
        ///     Specifies the routes that route to their controllers.
        /// </summary>
        /// <param name="routes"></param>
        public static void RegisterRoutes(RouteCollection routes){
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Default", "{controller}/{action}/{id}",
                new{
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                });
        }
    }
}
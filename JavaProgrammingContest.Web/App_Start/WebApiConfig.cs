using System.Web.Http;

namespace JavaProgrammingContest.Web.App_Start {
    /// <summary>
    ///     Configures the WebAPI routes.
    /// </summary>
    public static class WebApiConfig {
        /// <summary>
        ///     Registers the WebAPI route within the router.
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config) {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}

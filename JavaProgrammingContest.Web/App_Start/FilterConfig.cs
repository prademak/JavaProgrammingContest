using System.Web.Mvc;

namespace JavaProgrammingContest.Web.App_Start {
    /// <summary>
    ///     Configures Filters
    /// </summary>
    public static class FilterConfig {
        /// <summary>
        ///     Registers Instance specific filters.
        /// </summary>
        /// <param name="filters"></param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
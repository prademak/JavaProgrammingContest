using System.Web.Optimization;

namespace JavaProgrammingContest.Web.App_Start {
    /// <summary>
    ///     Internal Class.
    /// </summary>
    public static class BundleConfig {
        /// <summary>
        ///     Bundles all Javascript etc in one file and compresses them.
        ///     For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        /// </summary>
        /// <param name="bundles"></param>
        public static void RegisterBundles(BundleCollection bundles) {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/style.css"));

            // Bootstrap
            bundles.Add(new StyleBundle("~/Content/bootstrap").Include("~/Content/bootstrap.css"));
            bundles.Add(new ScriptBundle("~/Script/Bootstrap").Include("~/Scripts/bootstrap.js"));

            // CodeMirror Files
            bundles.Add(new StyleBundle("~/Content/CodeMirror").Include(
                        "~/Scripts/codemirror-2.35/lib/codemirror.css",
                        "~/Content/editor.css",

                        "~/Scripts/codemirror-2.35/theme/eclipse.css",
                        "~/Scripts/codemirror-2.35/theme/lesser-dark.css",
                        "~/Scripts/codemirror-2.35/theme/monokai.css"));

            bundles.Add(new ScriptBundle("~/Script/CodeMirror").Include(
                "~/Scripts/codemirror-2.35/lib/codemirror.js",
                "~/Scripts/codemirror-2.35/mode/clike/clike.js"));

            // Backbone
            bundles.Add(new ScriptBundle("~/Script/Backbone").Include(
                "~/Scripts/underscore.js",
                "~/Scripts/backbone.js",
                "~/Scripts/moment.js",
                "~/Scripts/json2.js"));

            // Backbone Application
            bundles.Add(new ScriptBundle("~/Script/Application").Include(
                "~/Scripts/application/models/assignment.js",
                "~/Scripts/application/models/codesubmit.js",
                "~/Scripts/application/models/participant.js",
                "~/Scripts/application/models/scoreboard.js",
                "~/Scripts/application/models/settings.js",
                "~/Scripts/application/models/progress.js",

                "~/Scripts/application/views/application.js",
                "~/Scripts/application/views/assignments.js",
                "~/Scripts/application/views/editor.js",
                "~/Scripts/application/views/console.js",
                "~/Scripts/application/views/timer.js",

                "~/Scripts/application/helper.js",
                "~/Scripts/application/builder.js",
                "~/Scripts/application/application.js"));

            // Noty
            bundles.Add(new ScriptBundle("~/Script/Noty").Include(
                "~/Scripts/noty/jquery.noty.js",
                "~/Scripts/noty/layouts/top.js",
                "~/Scripts/noty/layouts/topRight.js",
                "~/Scripts/noty/layouts/topCenter.js",
                "~/Scripts/noty/themes/default.js"));

            // QUnit
            bundles.Add(new StyleBundle("~/Content/QUnit").Include(
                       "~/Content/qunit.css"));
            bundles.Add(new ScriptBundle("~/Script/QUnit").Include(
                "~/Scripts/qunit.js",
                "~/Scripts/tests/testsuite.js"));
        }
    }
}
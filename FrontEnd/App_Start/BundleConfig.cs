namespace FrontEnd
{
    using System.Web.Optimization;

    /// <summary>
    /// empaquetado de archivos externos
    /// </summary>
    public class BundleConfig
    {
        /// <summary>
        /// empaquetado de archivos externos
        /// <paramref name="bundles"/>
        /// </summary>
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/loading.css",
                      "~/Content/font-awesome.min.css"));
        }
    }
}

using System.Web;
using System.Web.Optimization;

namespace RssAgregator.WEB
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/app").IncludeDirectory("~/app/", "*.js", true));

            bundles.Add(new ScriptBundle("~/bundles/jquerry").Include("~/Scripts/jquerry-2.2.2/jquery-2.2.2.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/toastr").Include("~/Scripts/toastr/toastr.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                        "~/Scripts/angular-1.4.8/angular-loader.js",
                        "~/Scripts/angular-1.4.8/angular.js",
                        "~/Scripts/angular-1.4.8/angular-animate.js",
                        "~/Scripts/angular-1.4.8/angular-resource.js",
                        "~/Scripts/angular-1.4.8/angular-route.js",
                        "~/Scripts/angular-1.4.8/angular-sanitize.js",
                        "~/Scripts/angular-1.4.8/angular-touch.js"));

            bundles.Add(new ScriptBundle("~/bundles/ui-bootstrap").Include("~/Scripts/ui-bootstrap-1.2.5/ui-bootstrap-tpls-1.2.5.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/soundmanagerv2").Include("~/Scripts/soundmanagerv2/soundmanager2-nodebug-jsmin.js"));


            bundles.Add(new StyleBundle("~/Content/cssLibs").Include(
                        "~/Content/libs/bootstrap-3.3.6/bootstrap-theme.css",
                        "~/Content/libs/bootstrap-3.3.6/bootstrap.css",
                        "~/Content/libs/toastr/toastr.css",
                        "~/Content/libs/angular-1.4.8/angular-csp.css",
                        "~/Content/libs/fontAwesome/font-awesome.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/main.css"));
        }
    }
}
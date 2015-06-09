using System.Web;
using System.Web.Optimization;

namespace KevinSharpWeb
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                // The Scripts
                //"~/Scripts/slider-revolution/js/jquery.themepunch.revolution.v45.js",
                "~/Scripts/slider-revolution/js/jquery.themepunch.revolution.v46.js",
                "~/Scripts/slider-revolution/js/jquery.themepunch.plugins.min.js",
                "~/Scripts/slider-revolution/js/jquery.themepunch.tools.min.js",
                "~/Scripts/jquery.parallax.js",
                "~/Scripts/jquery.wait.js",
                "~/Scripts/fappear.js",
                "~/Scripts/modernizr-2.6.2.min.js",
                "~/Scripts/jquery.bxslider.min.js",
                "~/Scripts/jquery.prettyPhoto.js",
                "~/Scripts/superfish.js",
                "~/Scripts/tweetMachine.js",
                "~/Scripts/tytabs.js",
                "~/Scripts/jquery.gmap.min.js",
                "~/Scripts/jquery.sticky.js",
                "~/Scripts/jquery.countTo.js",
                "~/Scripts/jflickrfeed.js",
                "~/Scripts/imagesloaded.pkgd.min.js",
                "~/Scripts/waypoints.min.js",
                "~/Scripts/wow.js",
                "~/Scripts/jquery.fitvids.js",
                "~/Scripts/custom.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                    // Library CSS
                    "~/Content/bootstrap.css",
                    "~/Content/bootstrap-theme.css",
                    "~/Content/fonts/font-awesome/css/font-awesome.css",
                    // Theme CSS
                    "~/Content/style.css",
                    // Responsive CSS
                    "~/Content/theme-responsive.css",                      
                    // Site's CSS
                    "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/fontawesome").Include("~/Content/fonts/font-awesome/css/font-awesome.css"));

                    bundles.Add(new StyleBundle("~/Content/screen").Include(
                    // Library CSS
                    "~/Content/animations.css",
                    "~/Content/superfish.css",
                    "~/Content/slider-revolution/css/settings.css",
                    "~/Content/slider-revolution/css/extralayers.css",
                    "~/Content/prettyPhoto.css",
                    "~/Content/jquery.bxslider.css"));
        }
    }
}

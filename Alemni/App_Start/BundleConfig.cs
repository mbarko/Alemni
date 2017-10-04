using System.Web;
using System.Web.Optimization;

namespace Alemni
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/lib").Include(
                        "~/Scripts/jquery-{version}.js",
                       
                      "~/Scripts/respond.js",
                      "~/Scripts/datatables/jquery.datatables.js",
                      "~/Scripts/datatables/datatables.bootstrap.js", 
                      "~/Scripts/datatables/datatables.responsive.js",
                       "~/Scripts/bootstrap.js",
                       "~/Scripts/bootbox.js",
                       "~/Scripts/jquery.bxslider.js", 
                       "~/Scripts/jquery-comments.js",
                       "~/Scripts/star-rating.js"




                       ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      
                      "~/Content/DataTables/css/dataTables.bootstrap.css",
                      "~/Content/bootstrap-united.css",
                    
                      "~/Content/DataTables/css/responsive.datatables.css", 
                      "~/Content/jquery.bxslider.css", "~/Content/jquery-comments.css",
                        "~/Content/site.css", "~/Content/ionicons/css/ionicons.css",
                          "~/Content/ionicons/css/ionicons.min.css",
                       "~/Content/normalize.css",
                       "~/Content/star-rating.css",
                       "~/Content/theme-krajee-fa.css"


                       ));
        }
    }
}

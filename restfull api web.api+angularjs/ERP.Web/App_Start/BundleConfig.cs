using System.Web;
using System.Web.Optimization;

namespace ERP.Web
{
    public class BundleConfig
    {
        // Дополнительные сведения об объединении см. по адресу: http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(
                new ScriptBundle("~/bundles/jquery").Include(
                    "~/Scripts/jquery-{version}.js",
                    "~/Scripts/jquery.loadmask.min.js",
                    "~/Scripts/fileuploader.js",
                    "~/Scripts/underscore.js",
                    "~/Scripts/Chart.js",
                    "~/Scripts/jquery.floatThead.js",
                    "~/Scripts/tablesort.js",
                    "~/Scripts/tablesort.number.js",
                    "~/Scripts/jquery.maskedinput.js",
                    "~/Scripts/jquery.filter_input.js",
                    "~/Scripts/CKEditor/ckeditor.js",
                    "~/Scripts/App/Core.js"));

            bundles.Add(
                new ScriptBundle("~/bundles/datatables").Include("~/Scripts/DataTables-1.10.3/jquery.DataTables.js", "~/Scripts/dataTables.bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/bootbox.js",
                      "~/Scripts/moment.js",
                      "~/Scripts/bootstrap-datepicker.js",
                      "~/Scripts/bootstrap-growl.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular")
                .IncludeDirectory("~/Scripts/Filters", "*.js")
                .IncludeDirectory("~/Scripts/Services", "*.js")
                .IncludeDirectory("~/Scripts/Controllers", "*.js")
                .Include(
                "~/Scripts/angular.js",
                "~/Scripts/angular-animate.js",
                "~/Scripts/angular-route.js",
                "~/Scripts/xeditable.js",
                "~/Scripts/loading-bar.js",
                "~/Scripts/angular-datatables.js",
                "~/Scripts/angular-multi-select.js",
                "~/Scripts/angular-input-modified.js",
                "~/Scripts/angular-form-validation.js",
                "~/Scripts/ng-tags-input.js",
                "~/Scripts/ui-grid.js",
                "~/Scripts/app.js"
                )
                .IncludeDirectory("~/Scripts/angular-ui", "*.js")
                .Include("~/Scripts/angular-ui/ui-bootstrap-tpls.js")
                .Include("~/Scripts/App/EmployeeApp.js")
                .Include("~/Scripts/App/AdminApp.js")
                .Include("~/Scripts/App/ERPApp.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-theme.css",
                      "~/Content/jquery.loadmask.css",
                      "~/Content/loading-bar.css",
                      "~/Content/datatables.bootstrap.css",
                      "~/Content/fileuploader.css",
                      "~/Content/angular-multi-select.css",
                      "~/Content/ng-tags-input.css",
                      "~/Content/font-awesome.css",
                      "~/Content/datepicker3.css",
                      "~/Content/xeditable.css",
                      "~/Content/ng-tags-input.css",
                      "~/Content/ng-tags-input.bootstrap.css",
                      "~/Content/ui-grid.css",
                      "~/Content/site.css"));

            BundleTable.EnableOptimizations = false;            
        }
    }
}

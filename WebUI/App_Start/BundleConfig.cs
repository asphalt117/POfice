using BundleTransformer.Core.Builders;
using BundleTransformer.Core.Bundles;
using BundleTransformer.Core.Orderers;
using BundleTransformer.Core.Resolvers;
using System.Web;
using System.Web.Optimization;

namespace WebUI
{
    public class BundleConfig
    {
        // Дополнительные сведения об объединении см. на странице https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Используйте версию Modernizr для разработчиков, чтобы учиться работать. Когда вы будете готовы перейти к работе,
            // готово к выпуску, используйте средство сборки по адресу https://modernizr.com, чтобы выбрать только необходимые тесты.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/content/css").Include(
                      "~/Content/bootstrap.css"));

            //bundles.Add(new ScriptBundle("~/bundles/main").Include(
            //         "~/Scripts/main.js"));

            bundles.Add(new StyleBundle("~/content/datepicker").Include(
                      "~/Content/datepicker.min.css"));
            bundles.Add(new ScriptBundle("~/bundles/datepicker").Include(
                      "~/Scripts/jquery-3.0.0.min.js",
                      "~/Scripts/datepicker.min.js"));

            bundles.Add(new StyleBundle("~/content/fa").Include(
                    "~/Content/fontawesome.css",
                    "~/Content/brands.css",
                     "~/Content/solid.css"));

            var nullBulider = new NullBuilder();
            var nullOrderer = new NullOrderer();

            BundleResolver.Current = new CustomBundleResolver();
            var commonStyleBundle = new CustomStyleBundle("~/content/maincss");

            commonStyleBundle.Include("~/Content/main.scss");
            commonStyleBundle.Orderer = nullOrderer;
            bundles.Add(commonStyleBundle);
        }
    }
}

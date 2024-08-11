using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace CMSNews.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundle(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css")
                   .Include("~/Content/bootstrap.min.css",
                   "~/Content/bootstrap-rtl.min.css",
                   "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundle/myScripts")
                   .Include("~/Scripts/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundle/jqueryval")
                  .Include("~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jquery")
                  .Include("~/Scripts/jquery-{version}.js"));

        }
    }
}
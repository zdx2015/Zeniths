using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Zeniths.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region js

            //bootstrap
            bundles.Add(new ScriptBundle("~/bootstrap-js").Include(
                        "~/plugin/jquery/jquery.js",
                        "~/plugin/bootstrap/js/bootstrap.js"));
 

            #endregion


            #region css

            //bootstrap
            bundles.Add(new StyleBundle("~/bootstrap-css").Include(
                      "~/plugin/bootstrap/css/bootstrap.css",
                      "~/plugin/font-awesome/css/font-awesome.css"));



            #endregion

            BundleTable.EnableOptimizations = true;
        }
    }
}
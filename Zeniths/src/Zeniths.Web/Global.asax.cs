using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Zeniths.WorkFlow.Utility;

namespace Zeniths.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var steps = WorkFlowHelper.GetNextSteps("b9a31bf8-7f94-453d-814a-ba8a2caa2ca2", "b9881411-83c2-4d97-8e5d-58e48befcff8");

        }
    }
}

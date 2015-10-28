using System.Web.Mvc;

namespace Zeniths.Web.Areas.WorkFlow
{
    public class WorkFlowAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "WorkFlow";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "WorkFlow_FlowStart",
                "WorkFlow/FlowRun/Start/{flowId}",
                new { controller = "FlowRun", action = "Start" }
            );

            context.MapRoute(
                "WorkFlow_FlowProcess",
                "WorkFlow/FlowRun/Process/{taskId}",
                new { controller = "FlowRun", action = "Process" }
            );

            context.MapRoute(
                "WorkFlow_default",
                "WorkFlow/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
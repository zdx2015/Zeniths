using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zeniths.WorkFlow.Service;
using Zeniths.WorkFlow.Utility;

namespace Zeniths.Web.Areas.WorkFlow.Controllers
{
    public class FlowTaskController : WorkFlowBaseController
    {
        private readonly FlowTaskService service = new FlowTaskService();

        /// <summary>
        /// 获取流程任务列表
        /// </summary>
        /// <param name="flowInstanceId">实例主键</param>
        /// <returns></returns>
        public ActionResult InstanceTasks(string flowInstanceId)
        {
            var list = service.GetTaskListByInstanceId(flowInstanceId);
            return View(list);
        }

        /// <summary>
        /// 获取流程图
        /// </summary>
        /// <param name="flowInstanceId"></param>
        /// <returns></returns>
        public ActionResult TaskImage(string flowInstanceId)
        {
            return View();
        }

        /// <summary>
        /// 获取待办任务
        /// </summary>
        /// <returns></returns>
        public ActionResult PendingTasks()
        {
            return View();
        }
        
        public ActionResult PendingTasksGrid(string flowId, string title)
        {
            return TasksGrid(flowId, title, 0);
        }

        /// <summary>
        /// 获取已办任务
        /// </summary>
        /// <returns></returns>
        public ActionResult HandledTasks()
        {
            return View();
        }

        public ActionResult HandledTasksGrid(string flowId,string title)
        {
            return TasksGrid(flowId, title, 0);
        }

        private ActionResult TasksGrid(string flowId ,string title, int type)
        {
            var pageIndex = GetPageIndex();
            var pageSize = GetPageSize();
            var orderName = GetOrderName();
            var orderDir = GetOrderDir();
            var list = service.GetTaskPageList(pageIndex, pageSize, orderName, orderDir,
                null,null, flowId, title, null,null, type);
            return View(list);
        }
    }
}
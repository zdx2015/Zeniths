using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Zeniths.Auth.Utility;
using Zeniths.Configuration;
using Zeniths.Extensions;
using Zeniths.Helper;
using Zeniths.Hr.Entity;
using Zeniths.Hr.Service;
using Zeniths.Utility;
using Zeniths.WorkFlow.Utility;

namespace Zeniths.Hr.WorkFlow.Reimburse
{
    [WorkFlowEventCaption("日常报销:填写费用报销单")]
    public class DailyReimburseStep1 : DefaultStepEvent
    {
        /// <summary>
        /// 保存数据方法
        /// </summary>
        public override BoolMessage OnSaveData(FlowEventArgs args)
        {
            var service = new DailyReimburseService();
            var entity = new Entity.DailyReimburse();
            ObjectHelper.SetObjectPropertys(entity, args.Form);//从表单数据中读取实体属性
            OrganizeHelper.SetCurrentUserInfo(entity);//设置登陆用户信息
            WorkFlowHelper.SetCurrentFlowInfo(entity, args);//设置业务表中流程信息

            entity.ReimburseDepartmentId = args.CurrentUser.DepartmentId;
            entity.ReimburseDepartmentName = args.CurrentUser.DepartmentName;
            entity.ApplicantId = args.CurrentUser.Id;
            entity.ApplicantName = args.CurrentUser.Name;
            entity.ApplicationDate = DateTime.Now.Date;
            entity.ApplySortNumber = service.GetYearAndMonthString() + "0000";
            //entity.Title = "填写报销单";
            entity.IsFinish = false;
            entity.FlowId = args.FlowId;
            //entity.FlowInstanceId = args.FlowInstanceId;
            entity.FlowName = "日常报销";
            // entity.StepId = args.StepId;
            // entity.StepName = "填写报销单";
            entity.BudgetId = service.GetBudgetId(args.CurrentUser.DepartmentId);
            entity.CreateUserId = args.CurrentUser.Id;
            entity.CreateUserName = args.CurrentUser.Name;
            entity.CreateDepartmentId = args.CurrentUser.DepartmentId;
            entity.CreateDepartmentName = args.CurrentUser.DepartmentName;
            entity.CreateDateTime = DateTime.Now;
            entity.ProjectSumMoney = 0;

            var curlist = JsonHelper.Deserialize<List<DailyReimburseDetails>>(args.Form["details"]);
            foreach (var item in curlist)
            {
                item.ReimburseId = entity.Id;
                entity.ProjectSumMoney = entity.ProjectSumMoney + item.Amount;
            }

            if (string.IsNullOrEmpty(args.ExecuteData.Title))
            {
                args.ExecuteData.Title = entity.Title = $"{args.CurrentUser.Name}的报销单({DateTimeHelper.FormatDate(entity.CreateDateTime)})";
            }

            //var result = entity.Id == 0 ? service.Insert(entity, curlist) : service.Update(entity, curlist);
            var result = service.Insert(entity, curlist);
            args.BusinessId = entity.Id.ToString();
            return result;
        }

        ///// <summary>
        ///// 流程提交后
        ///// </summary>
        //public override BoolMessage OnAfterSubmit(FlowEventArgs args)
        //{
        //    var service = new DailyReimburseService();
        //    var entity = service.Get(args.BusinessId.ToInt());
        //    entity.FlowInstanceId = args.FlowInstanceId;
        //    entity.StepId = args.StepId;
        //    entity.StepName = args.StepSetting.Name;
        //    entity.StepStatus = true;
        //    var list = (List<DailyReimburseDetails>)HttpContext.Current.Session["DailyReimburseDetails"];
        //    return service.Update(entity,0);
        //}
    }
}

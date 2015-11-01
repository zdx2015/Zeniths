using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Zeniths.Auth.Utility;
using Zeniths.Extensions;
using Zeniths.Helper;
using Zeniths.Hr.Entity;
using Zeniths.Hr.Service;
using Zeniths.Utility;
using Zeniths.WorkFlow.Utility;

namespace Zeniths.Hr.WorkFlow.Reimburse
{
    [WorkFlowEventCaption("日常报销:填写费用报销单")]
    public  class DailyReimburseStep1: DefaultStepEvent
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
            if (string.IsNullOrEmpty(args.ExecuteData.Title))
            {
                args.ExecuteData.Title = entity.Title = $"{args.CurrentUser.Name}的报销单({DateTimeHelper.FormatDate(entity.CreateDateTime)})";
            }
            var list = (List<DailyReimburseDetails>) HttpContext.Current.Session["DailyReimburseDetails"];
            var result = entity.Id == 0 ? service.Insert(entity,list) : service.Update(entity,list);
            args.BusinessId = entity.Id.ToString();
            return result;
        }

        /// <summary>
        /// 流程提交后
        /// </summary>
        public override BoolMessage OnAfterSubmit(FlowEventArgs args)
        {
            var service = new DailyReimburseService();
            var entity = service.Get(args.BusinessId.ToInt());
            entity.FlowInstanceId = args.FlowInstanceId;
            entity.StepId = args.StepId;
            entity.StepName = args.StepSetting.Name;
            entity.StepStatus = true;
            var list = (List<DailyReimburseDetails>)HttpContext.Current.Session["DailyReimburseDetails"];
            return service.Update(entity,0);
        }
    }
}

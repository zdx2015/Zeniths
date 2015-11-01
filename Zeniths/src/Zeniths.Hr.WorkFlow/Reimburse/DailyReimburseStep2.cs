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
    [WorkFlowEventCaption("日常报销:部门负责人签字确认")]
    public  class DailyReimburseStep2: DefaultStepEvent
    {
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
          
            entity.DepartmentManagerIsAudit = args.ExecuteData.IsAudit;
            entity.DepartmentManagerId = args.CurrentUser.Id;
            entity.DepartmentManagerSign = args.CurrentUser.Name;
            entity.DepartmentManagerOpinion = args.ExecuteData.Opinion;
            entity.DepartmentManagerSignDate = DateTime.Now;

            entity.StepStatus = entity.DepartmentManagerIsAudit.Value;
            var list = (List<DailyReimburseDetails>)HttpContext.Current.Session["DailyReimburseDetails"];
            return service.Update(entity,1);
        }
    }
}

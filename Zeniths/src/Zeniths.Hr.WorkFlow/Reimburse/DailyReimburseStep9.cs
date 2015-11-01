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
    [WorkFlowEventCaption("日常报销:总经理确认情况出示意见")]
    public class DailyReimburseStep9 : DefaultStepEvent
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

            entity.AddGeneralManagerIsAudit = args.ExecuteData.IsAudit;
            entity.AddGeneralManagerId = args.CurrentUser.Id;
            entity.AddGeneralManagerSign = args.CurrentUser.Name;
            entity.AddGeneralManagerOpinion = args.ExecuteData.Opinion;
            entity.AddGeneralManagerSignDate = DateTime.Now;

            entity.StepStatus = entity.AddGeneralManagerIsAudit.Value;
            var list = (List<DailyReimburseDetails>)HttpContext.Current.Session["DailyReimburseDetails"];
            return service.Update(entity, 8);
        }
    }
}

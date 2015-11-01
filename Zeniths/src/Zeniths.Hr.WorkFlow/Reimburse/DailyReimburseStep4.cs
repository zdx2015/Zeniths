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
    [WorkFlowEventCaption("日常报销:财务负责人审核")]
    public class DailyReimburseStep4: DefaultStepEvent
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
         
            entity.FinancialManagerIsAudit = args.ExecuteData.IsAudit;
            entity.FinancialManagerId = args.CurrentUser.Id;
            entity.FinancialManagerSign = args.CurrentUser.Name;
            entity.FinancialManagerOpinion = args.ExecuteData.Opinion;
            entity.FinancialManagerSignDate = DateTime.Now;

            entity.StepStatus = entity.FinancialManagerIsAudit.Value;
            var list = (List<DailyReimburseDetails>)HttpContext.Current.Session["DailyReimburseDetails"];
            return service.Update(entity, 3);
        }
    }
}

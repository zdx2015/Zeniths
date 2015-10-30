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
    public class DailyReimburseStep5 : DefaultStepEvent
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
        
            entity.GeneralManagerIsAudit = args.ExecuteData.IsAudit;
            entity.GeneralManagerId = args.CurrentUser.Id;
            entity.GeneralManagerSign = args.CurrentUser.Name;
            entity.GeneralManagerOpinion = args.ExecuteData.Opinion;
            entity.GeneralManagerSignDate = DateTime.Now;

            entity.StepStatus = entity.GeneralManagerIsAudit.Value;
            var list = (List<DailyReimburseDetails>)HttpContext.Current.Session["DailyReimburseDetails"];
            return service.Update(entity, 4);
        }
    }
}

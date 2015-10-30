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
    public class DailyReimburseStep6 : DefaultStepEvent
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
          
            entity.ChairmanIsAudit = args.ExecuteData.IsAudit;
            entity.ChairmanId = args.CurrentUser.Id;
            entity.ChairmanSign = args.CurrentUser.Name;
            entity.ChairmanOpinion = args.ExecuteData.Opinion;
            entity.ChairmanSignDate = DateTime.Now;

            entity.StepStatus = entity.ChairmanIsAudit.Value;
            var list = (List<DailyReimburseDetails>)HttpContext.Current.Session["DailyReimburseDetails"];
            return service.Update(entity, 5);
        }
    }
}

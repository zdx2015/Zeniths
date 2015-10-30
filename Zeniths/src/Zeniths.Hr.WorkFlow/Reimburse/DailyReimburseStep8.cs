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
   public class DailyReimburseStep8 : DefaultStepEvent
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
       
            entity.AddDepartmentManagerIsAudit = args.ExecuteData.IsAudit;
            entity.AddDepartmentManagerId = args.CurrentUser.Id;
            entity.AddDepartmentManagerSign = args.CurrentUser.Name;
            entity.AddDepartmentManagerOpinion = args.ExecuteData.Opinion;
            entity.AddDepartmentManagerSignDate = DateTime.Now;

            entity.StepStatus = entity.AddDepartmentManagerIsAudit.Value;
            var list = (List<DailyReimburseDetails>)HttpContext.Current.Session["DailyReimburseDetails"];
            return service.Update(entity, 7);
        }
    }
}

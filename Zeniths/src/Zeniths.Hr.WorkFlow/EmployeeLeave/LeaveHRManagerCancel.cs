using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zeniths.Auth.Utility;
using Zeniths.Extensions;
using Zeniths.Helper;
using Zeniths.Hr.Service;
using Zeniths.Utility;
using Zeniths.WorkFlow.Utility;

namespace Zeniths.Hr.WorkFlow.EmployeeLeave
{
    public class LeaveHRManagerCancel : DefaultStepEvent
    {
        /// <summary>
        /// 流程提交后
        /// </summary>
        public override BoolMessage OnAfterSubmit(FlowEventArgs args)
        {
            var service = new EmployeeLeaveService();
            var entity = service.Get(args.BusinessId.ToInt());

            entity.HRManagerId = args.CurrentUser.Id;
            entity.HRManagerIsAudit = true;
            entity.HRManagerSign = args.CurrentUser.Name;
            entity.HRManagerOpinion = args.ExecuteData.Opinion;
            entity.HRManagerSignDate = DateTime.Now;
            entity.IsFinish = true;
            entity.Status = "完成";

            return service.UpdateCancelLeaveHRManagerApproval(entity);
        }
    }
}

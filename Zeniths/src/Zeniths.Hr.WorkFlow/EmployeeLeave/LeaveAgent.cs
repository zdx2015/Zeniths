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
    public class LeaveAgent : DefaultStepEvent
    {
        /// <summary>
        /// 流程提交后
        /// </summary>
        public override BoolMessage OnAfterSubmit(FlowEventArgs args)
        {
            var service = new EmployeeLeaveService();
            var entity = service.Get(args.BusinessId.ToInt());
            
            entity.JobAgentId = args.CurrentUser.Id;
            entity.JobAgentIsAudit = true;
            entity.JobAgentSign = args.CurrentUser.Name;
            entity.JobAgentOpinion =args.ExecuteData.Opinion;
            entity.JobAgentSignDate = DateTime.Now;
            entity.Status = "审批中";

            return service.UpdateJobAgentApproval(entity);            
        }
    }
}

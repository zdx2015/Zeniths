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

namespace Zeniths.Hr.WorkFlow.EmployeeOvertime
{
    [WorkFlowEventCaption("加班:总经理意见")]
    public class OvertimeGeneralManager : DefaultStepEvent
    {
        /// <summary>
        /// 流程提交后
        /// </summary>
        public override BoolMessage OnAfterSubmit(FlowEventArgs args)
        {
            var service = new EmployeeOvertimeService();
            var entity = service.Get(args.BusinessId.ToInt());

            entity.GeneralManagerId = args.CurrentUser.Id;
            entity.GeneralManagerIsAudit = true;
            entity.GeneralManagerSign = args.CurrentUser.Name;
            entity.GeneralManagerOpinion = args.ExecuteData.Opinion;
            entity.GeneralManagerSignDate = DateTime.Now;
            entity.Status = 2;
            return service.UpdateGeneralManagerApproval(entity);
        }
    }
}

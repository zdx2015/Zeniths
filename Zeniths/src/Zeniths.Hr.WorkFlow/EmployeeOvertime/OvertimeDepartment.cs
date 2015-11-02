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
    [WorkFlowEventCaption("加班:部门负责人意见")]
    public class OvertimeDepartment : DefaultStepEvent
    {
        /// <summary>
        /// 流程提交后
        /// </summary>
        public override BoolMessage OnAfterSubmit(FlowEventArgs args)
        {
            var service = new EmployeeOvertimeService();
            var entity = service.Get(args.BusinessId.ToInt());
            
            entity.DepartmentManagerId = args.CurrentUser.Id;
            entity.DepartmentManagerIsAudit = true;
            entity.DepartmentManagerSign = args.CurrentUser.Name;
            entity.DepartmentManagerOpinion = args.ExecuteData.Opinion;
            entity.DepartmentManagerSignDate = DateTime.Now;
            entity.Status = 2;
            return service.UpdateDepartmentManagerApproval(entity);            
        }
    }
}

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
    public class LeaveDepartment : DefaultStepEvent
    {
        /// <summary>
        /// 流程提交后
        /// </summary>
        public override BoolMessage OnAfterSubmit(FlowEventArgs args)
        {
            var service = new EmployeeLeaveService();
            var entity = service.Get(args.BusinessId.ToInt());
            
            entity.DepartmentManagerId = args.CurrentUser.Id;
            entity.DepartmentManagerIsAudit = true;
            entity.DepartmentManagerSign = args.CurrentUser.Name;
            entity.DepartmentManagerOpinion = args.ExecuteData.Opinion;
            entity.DepartmentManagerSignDate = DateTime.Now;
            if (entity.Days > 5)
            {
                entity.Status = "审批中";
            }
            else
            {
                entity.Status = "销假中";
            }
            return service.UpdateDepartmentManagerApproval(entity);            
        }
    }
}

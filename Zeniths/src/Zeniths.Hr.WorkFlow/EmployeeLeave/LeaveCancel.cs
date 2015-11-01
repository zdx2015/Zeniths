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
    [WorkFlowEventCaption("请休假:销假信息确认")]
    public class LeaveCancel : DefaultStepEvent
    {
        /// <summary>
        /// 流程提交后
        /// </summary>
        public override BoolMessage OnAfterSubmit(FlowEventArgs args)
        {
            var service = new EmployeeLeaveService();
            var entity = service.Get(args.BusinessId.ToInt());

            entity.RealStartDatetime = args.Form["RealStartDatetime"].ToDateTime();
            entity.RealEndDatetime = args.Form["RealEndDatetime"].ToDateTime();
            entity.ActualDays = args.Form["ActualDays"].ToDouble();
            entity.CancelLeaveDateTime = DateTime.Now;
            entity.CancelLeavePersonId = args.CurrentUser.Id;
            entity.CancelLeavePersonName = args.CurrentUser.Name;
            entity.Status = 2;

            return service.UpdateCancelLeaveInfo(entity);
        }
    }
}

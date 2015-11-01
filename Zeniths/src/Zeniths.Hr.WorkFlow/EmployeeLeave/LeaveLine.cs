using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zeniths.Extensions;
using Zeniths.Hr.Service;
using Zeniths.Utility;
using Zeniths.WorkFlow.Utility;

namespace Zeniths.Hr.WorkFlow.EmployeeLeave
{
    /// <summary>
    /// 大于5天
    /// </summary>
    [WorkFlowEventCaption("请休假:大于5天")]
    public class LeaveGreaterThan5Line: DefaultLineEvent
    {
        public override BoolMessage OnValid(FlowLineEventArgs args)
        {
            var id = args.BusinessId.ToInt();
            
            EmployeeLeaveService service = new EmployeeLeaveService();
            var entity = service.Get(id);
            var days = entity.Days.Value;
            if (days > 5)
            {
                return BoolMessage.True;
            }
            return BoolMessage.False;
        }
    }

    /// <summary>
    /// 小于等于5天
    /// </summary>
    [WorkFlowEventCaption("请休假:小于等于5天")]
    public class LeaveLessThan5Line : DefaultLineEvent
    {
        public override BoolMessage OnValid(FlowLineEventArgs args)
        {
            var id = args.BusinessId.ToInt();
            EmployeeLeaveService service = new EmployeeLeaveService();
            var entity = service.Get(id);
            var days = entity.Days.Value;
            if (days <= 5)
            {
                return BoolMessage.True;
            }
            return BoolMessage.False;
        }
    }

}

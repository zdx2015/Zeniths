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
    /// 大于5
    /// </summary>
    public class LeaveeGreaterThan5Line: DefaultLineEvent
    {
        public override BoolMessage OnValid(FlowLineEventArgs args)
        {
            //var id = args.BusinessId.ToInt();
            //EmployeeLeaveService service = new EmployeeLeaveService();
            //var entity = service.Get(id);
            //var days = entity.Days.Value;
            //if (days > 5 )
            //{
            //    return BoolMessage.True;
            //}
            //return BoolMessage.False;
            return BoolMessage.True;
        }
    }

    /// <summary>
    /// 小于等于5
    /// </summary>
    public class LeaveeLessThan5Line : DefaultLineEvent
    {
        public override BoolMessage OnValid(FlowLineEventArgs args)
        {
            //var id = args.BusinessId.ToInt();
            //EmployeeLeaveService service = new EmployeeLeaveService();
            //var entity = service.Get(id);
            //var days = entity.Days.Value;
            //if (days <= 5)
            //{
            //    return BoolMessage.True;
            //}
            //return BoolMessage.False;
            return BoolMessage.True;
        }
    }

}

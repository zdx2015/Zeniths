﻿using Zeniths.Extensions;
using Zeniths.Utility;
using Zeniths.WorkFlow.Utility;

namespace Zeniths.Hr.WorkFlow.EmpLeave
{
    [WorkFlowEventCaption("请假:1天内")]
    public class EmpLeaveDaysLineLessThanOne : DefaultLineEvent
    {
        public override BoolMessage OnValid(FlowLineEventArgs args)
        {
            var days = args.Form["Days"].ToInt();
            if (days > 0 && days <= 1)
            {
                return BoolMessage.True;
            }
            return BoolMessage.False;
        }
    }

    public class EmpLeaveDaysLineGreaterThanOneAndLessThanThree : DefaultLineEvent
    {
        public override BoolMessage OnValid(FlowLineEventArgs args)
        {
            var days = args.Form["Days"].ToInt();
            if (days > 1 && days <= 3)
            {
                return BoolMessage.True;
            }
            return BoolMessage.False;
        }
    }

    [WorkFlowEventCaption("请假:大于3天")]
    public class EmpLeaveDaysLineGreaterThanThree : DefaultLineEvent
    {
        public override BoolMessage OnValid(FlowLineEventArgs args)
        {
            var days = args.Form["Days"].ToInt();
            if (days > 3)
            {
                return BoolMessage.True;
            }
            return BoolMessage.False;
        }
    }
}
using Zeniths.Extensions;
using Zeniths.Utility;
using Zeniths.WorkFlow.Utility;
using Zeniths.Hr.Entity;
using Zeniths.Hr.Service;
using System.Collections.Generic;

namespace Zeniths.Hr.WorkFlow.Reimburse
{
    [WorkFlowEventCaption("日常报销:预算内")]
    public class DailyReimburseInnerBudgetLine: DefaultLineEvent
    {
        public override BoolMessage OnValid(FlowLineEventArgs args)
        {
            var result = new BoolMessage(true);
            //var service = new DailyReimburseDetailsService();
            //result = service.IsInnerBudget(args.BusinessId.ToInt(), args.CurrentUser.DepartmentId);
            return result;
        }
    }


    [WorkFlowEventCaption("日常报销:预算外")]
    public class DailyReimburseOutBudgetLine : DefaultLineEvent
    {
        public override BoolMessage OnValid(FlowLineEventArgs args)
        {
            var result = new BoolMessage(true);
            //var service = new DailyReimburseDetailsService();
            //result = service.IsInnerBudget(args.BusinessId.ToInt(), args.CurrentUser.DepartmentId);
            result = new BoolMessage(!result.Success);
            return result;
           
        }
    }

    [WorkFlowEventCaption("日常报销:报销大于等于5万")]
    public class DailyReimburseMoreThan50000Line : DefaultLineEvent
    {
        public override BoolMessage OnValid(FlowLineEventArgs args)
        {
            var result = new BoolMessage(true);
            //var service = new DailyReimburseDetailsService();
            //result = service.IsInnerBudget(args.BusinessId.ToInt(), args.CurrentUser.DepartmentId);
            return result;
        }
    }

    [WorkFlowEventCaption("日常报销:报销小于5万")]
    public class DailyReimburseLessThan50000Line : DefaultLineEvent
    {
        public override BoolMessage OnValid(FlowLineEventArgs args)
        {
            var result = new BoolMessage(true);
            //var service = new DailyReimburseDetailsService();
            //result = service.IsInnerBudget(args.BusinessId.ToInt(), args.CurrentUser.DepartmentId);
            return result;
        }
    }

}

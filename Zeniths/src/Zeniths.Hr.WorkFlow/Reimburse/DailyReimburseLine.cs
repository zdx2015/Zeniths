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
            //var service = new DailyReimburseDetailsService();
            //var list = service.GetList(args.ExecuteData.BusinessId.ToInt());
            //var budList = service.GetBudgetDetailList(args.CurrentUser.DepartmentId);
            //List<bool> results = new List<bool>();
            //bool finalResult = false;
            //if (list != null && budList != null)
            //{
            //    if (list.Count > budList.Count)
            //    {
            //        return new BoolMessage(finalResult);
            //    }

            //    for (int i = 0; i < list.Count; i++)
            //    {
            //        foreach (var item in budList)
            //        {
            //            if (list[i].CategoryId == item.BudgetCategoryId)
            //            {
            //                if (list[i].ItemId == item.BudgetItemId)
            //                {
            //                    if (list[i].Amount == item.BudgetMoney)
            //                    {
            //                        results.Add(true);
            //                    }
            //                }
            //            }
            //        }

            //    }

            //    if (results.Count == list.Count)
            //    {
            //        finalResult = true;
            //    }
               
            //}
            //return new BoolMessage(finalResult);
            return BoolMessage.True;
        }
    }


    [WorkFlowEventCaption("日常报销:预算外")]
    public class DailyReimburseOutBudgetLine : DefaultLineEvent
    {
        public override BoolMessage OnValid(FlowLineEventArgs args)
        {
            //var service = new DailyReimburseDetailsService();
            //var list = service.GetList(args.ExecuteData.BusinessId.ToInt());
            //var budList = service.GetBudgetDetailList(args.CurrentUser.DepartmentId);
            //List<bool> results = new List<bool>();
            //bool finalResult = false;
            //if (list != null && budList != null)
            //{
            //    if (list.Count > budList.Count)
            //    {
            //        return new BoolMessage(finalResult);
            //    }

            //    for (int i = 0; i < list.Count; i++)
            //    {
            //        foreach (var item in budList)
            //        {
            //            if (list[i].CategoryId == item.BudgetCategoryId)
            //            {
            //                if (list[i].ItemId == item.BudgetItemId)
            //                {
            //                    if (list[i].Amount == item.BudgetMoney)
            //                    {
            //                        results.Add(true);
            //                    }
            //                }
            //            }
            //        }

            //    }

            //    if (results.Count == list.Count)
            //    {
            //        finalResult = true;
            //    }

            //    finalResult = !finalResult;

            //}
            //return new BoolMessage(finalResult);
            return BoolMessage.False;
        }
    }

}

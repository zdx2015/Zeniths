﻿using System;
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

namespace Zeniths.Hr.WorkFlow.Budget
{
    public class BudgetLeaveRegister : DefaultStepEvent
    {
        public override BoolMessage OnSaveData(FlowEventArgs args)
        {
            var service = new HrBudgetService();
            var entity = new Entity.HrBudget();
            ObjectHelper.SetObjectPropertys(entity, args.Form);//从表单数据中读取实体属性
            OrganizeHelper.SetCurrentUserInfo(entity);//设置登陆用户信息
            WorkFlowHelper.SetCurrentFlowInfo(entity, args);//设置业务表中流程信息
            if (string.IsNullOrEmpty(args.ExecuteData.Title))
            {
                args.ExecuteData.Title = entity.Title = $"{args.CurrentUser.DepartmentName}的预算申请({DateTimeHelper.FormatDate(entity.CreateDateTime)})";
            }
            var result = entity.Id == 0 ? service.Insert(entity) : service.Update(entity);
            args.BusinessId = entity.Id.ToString();
            return result;
        }
        /// <summary>
        /// 流程提交后
        /// </summary>
        public override BoolMessage OnAfterSubmit(FlowEventArgs args)
        {
            var service = new EmpLeaveService();
            var entity = service.Get(args.BusinessId.ToInt());
            entity.FlowInstanceId = args.FlowInstanceId;
            entity.StepId = args.StepId;
            entity.StepName = args.StepSetting.Name;
            return service.Update(entity);
        }
    }
}
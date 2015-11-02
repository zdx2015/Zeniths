using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Zeniths.Auth.Utility;
using Zeniths.Extensions;
using Zeniths.Helper;
using Zeniths.Hr.Entity;
using Zeniths.Hr.Service;
using Zeniths.Utility;
using Zeniths.WorkFlow.Utility;

namespace Zeniths.Hr.WorkFlow.Contract
{
    [WorkFlowEventCaption("合同管理:录入上传合同相关内容")]
    public class ContractStep_Send1 : DefaultStepEvent
    {
        public override BoolMessage OnSaveData(FlowEventArgs args)
        {
            var service = new ContractService();
            var entity = new Entity.Contract();
            ObjectHelper.SetObjectPropertys(entity, args.Form);//从表单数据中读取实体属性
            OrganizeHelper.SetCurrentUserInfo(entity);//设置登陆用户信息
            WorkFlowHelper.SetCurrentFlowInfo(entity, args);//设置业务表中流程信息

            if (entity.IsChange)
            {
                var haschangeno = service.ExistsContractNo(entity);//检测原合同编号是否符合要求
                if (haschangeno.Failure)
                {
                    return haschangeno;
                }
            }
            var hasResult = service.Exists(entity);//存在返回
            if (hasResult.Failure)
            {
                OrganizeHelper.SetCurrentUserCreateInfo(entity);
                entity.UndertakeDepartmentID = entity.CreateDepartmentId;//承办部门默认与起草人登陆用户所在部门相同
                entity.UndertakeDepartmentName = entity.CreateDepartmentName;
                entity.SenderID = entity.CreateUserId;//送审人默认与创建人相同
                entity.SenderName = entity.CreateUserName;
            }
                entity.StateId = "0";
            entity.StepDateTime = DateTime.Now;
            WorkFlowHelper.SetCurrentFlowInfo(entity, args);//设置业务表中流程信息
            
            var result = entity.Id == 0 ? service.Insert(entity) : service.Update(entity);
            args.BusinessId = entity.Id.ToString();
            return result;

        }
        public override BoolMessage OnAfterSubmit(FlowEventArgs args)
        {
            var service = new ContractService();
            var entity = service.Get(args.BusinessId.ToInt());
            entity.FlowInstanceId = args.FlowInstanceId;
            entity.StepId = args.StepId;
            entity.StepName = args.StepSetting.Name;
            return service.Update(entity);
        }
    }
}

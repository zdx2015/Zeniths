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
            return base.OnSaveData(args);
        }
        public override BoolMessage OnAfterSubmit(FlowEventArgs args)
        {
            return base.OnAfterSubmit(args);
        }
    }
}

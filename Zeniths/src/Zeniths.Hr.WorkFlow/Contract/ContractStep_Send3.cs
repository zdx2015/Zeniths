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
    [WorkFlowEventCaption("合同管理:承办部门负责人审核")]
    public class ContractStep_Send3 : DefaultStepEvent
    {
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

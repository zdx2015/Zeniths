using Zeniths.Auth.Utility;
using Zeniths.Helper;
using Zeniths.Utility;
using Zeniths.WorkFlow.Utility;

namespace Zeniths.Hr.WorkFlow.EmpLeave
{
    public class EmpLeaveRegister : DefaultStepEvent
    {
        public override BoolMessage OnSaveData(FlowEventArgs args)
        {
            var entity = new Entity.EmpLeave();
            ObjectHelper.SetObjectPropertys(entity, args.Form);//从表单数据中读取实体属性
            OrganizeHelper.SetCurrentUserInfo(entity);//设置登陆用户信息
            WorkFlowHelper.SetCurrentFlowInfo(entity, args);//设置业务表中流程信息

            return base.OnSaveData(args);
        }
    }
}
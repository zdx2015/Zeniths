using System.Collections.Generic;
using Newtonsoft.Json;

namespace Zeniths.WorkFlow.Utility
{
    /// <summary>
    /// 流程设计对象
    /// </summary>
    public class WorkFlowDesign
    {
        /// <summary>
        /// 流程属性
        /// </summary>
        [JsonProperty("property")]
        public FlowProperty Property { get; set; }

        /// <summary>
        /// 流程设计图
        /// </summary>
        [JsonProperty("flow")]
        public FlowDesign Flow { get; set; }

        /// <summary>
        /// 步骤设置
        /// </summary>
        [JsonProperty("step")]
        public Dictionary<string, FlowStepSetting> Step { get; set; } = new Dictionary<string, FlowStepSetting>();

        /// <summary>
        /// 流程线设置
        /// </summary>
        [JsonProperty("line")]
        public Dictionary<string, FlowLineSetting> Line { get; set; } = new Dictionary<string, FlowLineSetting>();

    }

    /// <summary>
    /// 流程属性
    /// </summary>
    public class FlowProperty
    {
        /// <summary>
        /// 流程标识
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }
        
        /// <summary>
        /// 流程名称
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// 流程分类
        /// </summary>
        [JsonProperty("category")]
        public string Category { get; set; }

        /// <summary>
        /// 是否发布
        /// </summary>
        [JsonProperty("isEnabled")]
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 是否启用调试
        /// </summary>
        [JsonProperty("isDebug")]
        public bool IsDebug { get; set; }

        /// <summary>
        /// 调试用户字符串
        /// </summary>
        [JsonProperty("debugUserIds")]
        public string DebugUserIds { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        [JsonProperty("sortIndex")]
        public int SortIndex { get; set; }

        /// <summary>
        /// 备注说明
        /// </summary>
        [JsonProperty("note")]
        public string Note { get; set; }
    }

    /// <summary>
    /// 流程设计图
    /// </summary>
    public class FlowDesign
    {
        /// <summary>
        /// 标题
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// 步骤集合
        /// </summary>
        [JsonProperty("nodes")]
        public Dictionary<string, FlowStep> Steps { get; set; } = new Dictionary<string, FlowStep>();

        /// <summary>
        /// 线集合
        /// </summary>
        [JsonProperty("lines")]
        public Dictionary<string, FlowLine> Lines { get; set; } = new Dictionary<string, FlowLine>();

        /// <summary>
        /// 区域集合
        /// </summary>
        [JsonProperty("areas")]
        public Dictionary<string, FlowArea> Areas { get; set; } = new Dictionary<string, FlowArea>();

        /// <summary>
        /// 对象数量计数
        /// </summary>
        [JsonProperty("initNum")]
        public int InitNum { get; set; }
    }

    /// <summary>
    /// 步骤
    /// </summary>
    public class FlowStep
    {
        /// <summary>
        /// 步骤Id
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// 步骤UUID
        /// </summary>
        [JsonProperty("uid")]
        public string Uid { get; set; }

        /// <summary>
        /// 步骤名称
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// 左边距
        /// </summary>
        [JsonProperty("left")]
        public int Left { get; set; }

        /// <summary>
        /// 上边距
        /// </summary>
        [JsonProperty("top")]
        public int Top { get; set; }

        /// <summary>
        /// 节点类型 (startround:开始;endround:endround;stepnode:普通节点;shuntnode:分流节点;confluencenode:合流节点)
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// 宽度
        /// </summary>
        [JsonProperty("width")]
        public int Width { get; set; }

        /// <summary>
        /// 高度
        /// </summary>
        [JsonProperty("height")]
        public int Height { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("alt")]
        public bool Alt { get; set; }
    }

    /// <summary>
    /// 步骤设置
    /// </summary>
    public class FlowStepSetting
    {
        /// <summary>
        /// 步骤名称
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// 编程标识
        /// </summary>
        [JsonProperty("uid")]
        public string Uid { get; set; }

        /// <summary>
        /// 步骤名称
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// 审签类型
        /// <para>
        ///  0:无签批意见栏
        ///  1:有签批意见栏(无须签章)
        ///  2:有签批意见栏(须签章)
        /// </para>
        /// </summary>
        [JsonProperty("signatureType")]
        public string SignatureType { get; set; }

        /// <summary>
        /// 警告工时
        /// </summary>
        [JsonProperty("warningHour")]
        public string WarningHour { get; set; }

        /// <summary>
        /// 处理工时
        /// </summary>
        [JsonProperty("processHour")]
        public string ProcessHour { get; set; }

        /// <summary>
        /// 表单Id
        /// </summary>
        [JsonProperty("formName")]
        public string FormName { get; set; }

        /// <summary>
        /// 步骤
        /// </summary>
        [JsonProperty("note")]
        public string Note { get; set; }

        /// <summary>
        /// 流转类型
        /// <para>
        /// 0:系统控制
        /// 1:单选一个分支流转
        /// 2:多选几个分支流转
        /// </para>
        /// </summary>
        [JsonProperty("flowCategory")]
        public string FlowCategory { get; set; }

        /// <summary>
        /// 处理策略
        /// <para>
        /// 0:一人同意即可
        /// 1:所有人必须同意
        /// 2:独立处理
        /// 3:依据比例
        /// </para>
        /// </summary>
        [JsonProperty("processPolicy")]
        public string ProcessPolicy { get; set; }

        /// <summary>
        /// 处理比例
        /// </summary>
        [JsonProperty("processPercentage")]
        public string ProcessPercentage { get; set; }

        /// <summary>
        /// 会签策略
        /// <para>
        /// 0:不会签
        /// 1:一个步骤同意即可
        /// 2:所有步骤同意
        /// 3:依据比例
        /// </para>
        /// </summary>
        [JsonProperty("countersignPolicy")]
        public string CountersignPolicy { get; set; }

        /// <summary>
        /// 会签比例
        /// </summary>
        [JsonProperty("countersignPercentage")]
        public string CountersignPercentage { get; set; }

        /// <summary>
        /// 退回策略
        /// <para>
        /// 0:根据处理策略退回
        /// 1:不能退回
        /// </para>
        /// </summary>
        [JsonProperty("backPolicy")]
        public string BackPolicy { get; set; }

        /// <summary>
        /// 退回类型
        /// <para>
        /// 0:退回前一步
        /// 1:退回第一步
        /// 2:退回某一步
        /// </para>
        /// </summary>
        [JsonProperty("backCategory")]
        public string BackCategory { get; set; }

        /// <summary>
        /// 退回步骤UUID
        /// </summary>
        [JsonProperty("backStep")]
        public string BackStep { get; set; }

        /// <summary>
        /// 选择策略
        /// <para>
        /// 0:自动
        /// 1:允许
        /// 2:不允许
        /// </para>
        /// </summary>
        [JsonProperty("handlerSelectPolicy")]
        public string HandlerSelectPolicy { get; set; }

        /// <summary>
        /// 处理者类型
        /// <para>
        /// 0:所有成员
        /// 1:部门
        /// 2:人员
        /// 3:发起者
        /// 4:发起者领导
        /// 5:发起者分管领导
        /// 6:前一步骤处理者
        /// 7:前一步处理者领导
        /// 8:前一步处理者分管领导
        /// </para>
        /// </summary>
        [JsonProperty("handlerCategory")]
        public string HandlerCategory { get; set; }

        /// <summary>
        /// 选择范围
        /// </summary>
        [JsonProperty("handlerSelectRange")]
        public string HandlerSelectRange { get; set; }

        /// <summary>
        /// 默认处理者
        /// </summary>
        [JsonProperty("handlerDefault")]
        public string HandlerDefault { get; set; }

        /// <summary>
        /// 保存数据事件
        /// </summary>
        [JsonProperty("eventSaveFromData")]
        public string EventSaveFromData { get; set; }

        /// <summary>
        /// 提交前事件
        /// </summary>
        [JsonProperty("eventSubmitBefore")]
        public string EventSubmitBefore { get; set; }

        /// <summary>
        /// 提交后事件
        /// </summary>
        [JsonProperty("eventSubmitAfter")]
        public string EventSubmitAfter { get; set; }

        /// <summary>
        /// 退回前事件
        /// </summary>
        [JsonProperty("eventBackBefore")]
        public string EventBackBefore { get; set; }

        /// <summary>
        /// 退回后事件
        /// </summary>
        [JsonProperty("eventBackAfter")]
        public string EventBackAfter { get; set; }

        /// <summary>
        /// 是否启用超时提醒
        /// </summary>
        [JsonProperty("expiredPrompt")]
        public bool ExpiredPrompt { get; set; }
    }

    /// <summary>
    /// 流程线
    /// </summary>
    public class FlowLine
    {
        /// <summary>
        /// 线标识
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// 线类型(sl:直线;lr:左右移动型折线;tb:上下移动型折线)
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// 源步骤标识
        /// </summary>
        [JsonProperty("from")]
        public string From { get; set; }

        /// <summary>
        /// 目标步骤标识
        /// </summary>
        [JsonProperty("to")]
        public string To { get; set; }

        /// <summary>
        /// 线显示名称
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("alt")]
        public bool Alt { get; set; }
    }

    /// <summary>
    /// 流程线配置
    /// </summary>
    public class FlowLineSetting
    {
        /// <summary>
        /// 线显示名称
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// 自定义方法
        /// </summary>
        [JsonProperty("method")]
        public string Method { get; set; }

        /// <summary>
        /// 发送者属于
        /// </summary>
        [JsonProperty("organize_senderin")]
        public string OrganizeSenderin { get; set; }

        /// <summary>
        /// 发送者不属于
        /// </summary>
        [JsonProperty("organize_sendernotin")]
        public string OrganizeSendernotin { get; set; }

        /// <summary>
        /// 发起者属于
        /// </summary>
        [JsonProperty("organize_sponsorin")]
        public string OrganizeSponsorin { get; set; }

        /// <summary>
        /// 发起者不属于
        /// </summary>
        [JsonProperty("organize_sponsornotin")]
        public string OrganizeSponsornotin { get; set; }

        /// <summary>
        /// 发送者是部门领导
        /// </summary>
        [JsonProperty("organize_senderleader")]
        public bool OrganizeSenderleader { get; set; }

        /// <summary>
        /// 发送者是部门分管领导
        /// </summary>
        [JsonProperty("organize_senderchargeleader")]
        public bool OrganizeSenderchargeleader { get; set; }

        /// <summary>
        /// 发起者是
        /// </summary>
        [JsonProperty("organize_sponsorleader")]
        public bool OrganizeSponsorleader { get; set; }

        /// <summary>
        /// 发起者是
        /// </summary>
        [JsonProperty("organize_sponsorchargeleader")]
        public bool OrganizeSponsorchargeleader { get; set; }

        /// <summary>
        /// 发送者不是
        /// </summary>
        [JsonProperty("organize_notsenderleader")]
        public bool OrganizeNotsenderleader { get; set; }

        /// <summary>
        /// 发送者不是
        /// </summary>
        [JsonProperty("organize_notsenderchargeleader")]
        public bool OrganizeNotsenderchargeleader { get; set; }

        /// <summary>
        /// 发起者不是
        /// </summary>
        [JsonProperty("organize_notsponsorleader")]
        public bool OrganizeNotsponsorleader { get; set; }

        /// <summary>
        /// 发起者不是
        /// </summary>
        [JsonProperty("organize_notsponsorchargeleader")]
        public bool OrganizeNotsponsorchargeleader { get; set; }
    }

    /// <summary>
    /// 流程图区域对象
    /// </summary>
    public class FlowArea
    {
        /// <summary>
        /// 区域名称
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// 左边距
        /// </summary>
        [JsonProperty("left")]
        public int Left { get; set; }

        /// <summary>
        /// 上边距
        /// </summary>
        [JsonProperty("top")]
        public int Top { get; set; }

        /// <summary>
        /// 颜色
        /// </summary>
        [JsonProperty("color")]
        public string Color { get; set; }

        /// <summary>
        /// 宽度
        /// </summary>
        [JsonProperty("width")]
        public int Width { get; set; }

        /// <summary>
        /// 高度
        /// </summary>
        [JsonProperty("height")]
        public int Height { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("alt")]
        public bool Alt { get; set; }
    }
}
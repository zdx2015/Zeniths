using System.Collections.Generic;
using Newtonsoft.Json;

namespace Zeniths.WorkFlow.Utility
{
    /// <summary>
    /// 执行参数
    /// </summary>
    public class ExecuteParam
    {
        /// <summary>
        /// 执行类型
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// 提交步骤
        /// </summary>
        [JsonProperty("steps")]
        public List<ExecuteParamStep> Steps { get; set; } = new List<ExecuteParamStep>();
    }

    /// <summary>
    /// 执行参数步骤
    /// </summary>
    public class ExecuteParamStep
    {
        /// <summary>
        /// 步骤Id
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// 成员字符串,逗号隔开
        /// </summary>
        [JsonProperty("member")]
        public string Member { get; set; }
    }
}
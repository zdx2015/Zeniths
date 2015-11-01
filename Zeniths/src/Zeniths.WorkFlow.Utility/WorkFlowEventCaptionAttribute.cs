using System;
using Zeniths.Helper;

namespace Zeniths.WorkFlow.Utility
{
    /// <summary>
    /// 工作流事件描述
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class WorkFlowEventCaptionAttribute : Attribute
    {
        private string _provider;

        /// <summary>
        /// 初始化工作流事件描述
        /// </summary>
        /// <param name="cation">事件描述</param>
        public WorkFlowEventCaptionAttribute(string cation)
        {
            Cation = cation;
        }

        /// <summary>
        /// 描述
        /// </summary>
        public string Cation { get; set; }

        /// <summary>
        /// 实现类
        /// </summary>
        public Type InstanceType { get; internal set; }

        /// <summary>
        /// 实现类字符串
        /// </summary>
        public string Provider
        {
            get
            {
                if (string.IsNullOrEmpty(_provider))
                {
                    _provider = AssemblyHelper.GetTypeFullName(InstanceType);
                }
                return _provider;
            }
        }
    }
}
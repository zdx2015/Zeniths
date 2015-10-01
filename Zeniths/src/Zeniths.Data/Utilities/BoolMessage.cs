// ===============================================================================
// Copyright (c) 2015 正得信集团股份有限公司
// ===============================================================================
using System;

namespace Zeniths.Data.Utilities
{
    /// <summary>
    /// 状态消息
    /// </summary>
    public class StatusMessage
    {
        private readonly bool _success;
        private readonly string _message;
        private readonly Exception _exptionObject;

        /// <summary>
        /// 初始化状态消息
        /// </summary>
        /// <param name="success">布尔状态</param>
        public StatusMessage(bool success)
        {
            _success = success;
        }

        /// <summary>
        /// 初始化状态消息
        /// </summary>
        /// <param name="success">布尔状态</param>
        /// <param name="message">状态消息</param>
        public StatusMessage(bool success, string message)
        {
            _success = success;
            _message = message;
        }

        /// <summary>
        /// 初始化状态消息
        /// </summary>
        /// <param name="success">布尔状态</param>
        /// <param name="message">状态消息</param>
        /// <param name="exptionObject">异常对象</param>
        public StatusMessage(bool success, string message, Exception exptionObject)
        {
            _exptionObject = exptionObject;
            _message = message;
            _success = success;
        }

        /// <summary>
        /// 成功状态
        /// </summary>
        public bool Success
        {
            get { return _success; }
        }
        
        /// <summary>
        /// 状态消息
        /// </summary>
        public string Message
        {
            get { return _message; }
        }

        /// <summary>
        /// 异常对象
        /// </summary>
        public Exception ExptionObject
        {
            get { return _exptionObject; }
        }
    }
}
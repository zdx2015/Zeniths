// ===============================================================================
// Copyright (c) 2015 正得信集团股份有限公司
// ===============================================================================
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.Mvc;

namespace Zeniths.MvcUtility
{
    /// <summary>
    /// 基础控制器
    /// </summary>
    public class JsonController : Controller
    {
        /// <summary>
        /// 创建一个将指定对象序列化为 JavaScript 对象表示法 (JSON) 的 System.Web.Mvc.JsonResult 对象。
        /// </summary>
        /// <param name="data">要序列化的 JavaScript 对象图。</param>
        /// <returns>将指定对象序列化为 JSON 格式的 JSON 结果对象。在执行此方法所准备的结果对象时，ASP.NET MVC 框架会将该对象写入响应。</returns>
        protected JsonNetResult JsonNet(object data)
        {
            return new JsonNetResult(data);
        }

        /// <summary>
        /// 创建 System.Web.Mvc.JsonResult 对象，该对象使用内容类型、内容编码和 JSON 请求行为将指定对象序列化为 JavaScript 对象表示法 (JSON) 格式。
        /// </summary>
        /// <param name="data">要序列化的 JavaScript 对象图。</param>
        /// <param name="contentType">内容类型（MIME 类型）。</param>
        /// <returns>将指定对象序列化为 JSON 格式的结果对象。</returns>
        protected JsonNetResult JsonNet(object data, string contentType)
        {
            return new JsonNetResult(data, contentType);
        }

        /// <summary>
        /// 创建 System.Web.Mvc.JsonResult 对象，该对象使用内容类型、内容编码和 JSON 请求行为将指定对象序列化为 JavaScript 对象表示法 (JSON) 格式。
        /// </summary>
        /// <param name="data">要序列化的 JavaScript 对象图。</param>
        /// <param name="contentType">内容类型（MIME 类型）。</param>
        /// <param name="contentEncoding">内容编码。</param>
        /// <returns>将指定对象序列化为 JSON 格式的结果对象。</returns>
        protected JsonNetResult JsonNet(object data, string contentType, Encoding contentEncoding)
        {
            return new JsonNetResult(data, contentType, contentEncoding);
        }

        /// <summary>
        /// 创建 System.Web.Mvc.JsonResult 对象，该对象使用内容类型、内容编码和 JSON 请求行为将指定对象序列化为 JavaScript 对象表示法 (JSON) 格式。
        /// </summary>
        /// <param name="data">要序列化的 JavaScript 对象图。</param>
        /// <param name="contentType">内容类型（MIME 类型）。</param>
        /// <param name="contentEncoding">内容编码。</param>
        /// <param name="behavior">JSON 请求行为</param>
        /// <returns>将指定对象序列化为 JSON 格式的结果对象。</returns>
        protected JsonNetResult JsonNet(object data, string contentType, Encoding contentEncoding,
            JsonRequestBehavior behavior)
        {
            return new JsonNetResult(data, contentType, contentEncoding, behavior);
        }

        /// <summary>
        /// 创建一个将指定对象序列化为 JavaScript 对象表示法 (JSON) 的 System.Web.Mvc.JsonResult 对象。响应为text/html
        /// </summary>
        /// <param name="data">要序列化的 JavaScript 对象图。</param>
        /// <returns>将指定对象序列化为 JSON 格式的 JSON 结果对象。在执行此方法所准备的结果对象时，ASP.NET MVC 框架会将该对象写入响应。</returns>
        protected JsonNetResult JsonNetText(object data)
        {
            return new JsonNetResult(data, "text/html", Encoding.UTF8);
        }

        /// <summary>
        /// 服务器404错误消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public JsonNetResult NotFound(string msg)
        {
            return new JsonNetResult(new JsonMessage(false, "找不到网页:"+msg));
        }

        /// <summary>
        /// 服务器内部500错误消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public JsonNetResult InternalError(string msg)
        {
            return new JsonNetResult(new JsonMessage(false, msg));
        }
        
    }
}

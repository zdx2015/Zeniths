// ===============================================================================
// Copyright (c) 2015 正得信集团股份有限公司
// ===============================================================================
using System;
using System.Web.Mvc;

namespace Zeniths.MvcUtility
{
    /// <summary>
    /// 全局Json错误异常捕获
    /// </summary>
    public class JsonExceptionAttribute : HandleErrorAttribute
    {
        /// <summary>
        /// 在发生异常时调用。
        /// </summary>
        /// <param name="filterContext">筛选器上下文。</param>
        public override void OnException(ExceptionContext filterContext)
        {
            var requestedWith = filterContext.HttpContext.Request.Headers["X-Requested-With"];
            if (!string.IsNullOrEmpty(requestedWith) && requestedWith.Equals("XMLHttpRequest"))
            {
                var httpException = filterContext.Exception;
                if (httpException != null)
                {
                    filterContext.Result = new JsonNetResult(new JsonMessage(false, httpException.Message.Replace("\r", "").Replace("\n", ""))); // new BaseController().InternalError(httpException.Message);
                    filterContext.ExceptionHandled = true;
                    filterContext.HttpContext.Response.Clear();
                    filterContext.HttpContext.Response.StatusCode = 500;
                    filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
                }
            }
            else
            {
                base.OnException(filterContext);
            }
        }
    }
}

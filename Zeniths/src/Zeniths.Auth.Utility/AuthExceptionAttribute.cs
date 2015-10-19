using System.Web.Mvc;
using Zeniths.MvcUtility;

namespace Zeniths.Auth.Utility
{
    public class AuthExceptionAttribute : JsonExceptionAttribute
    {
        /// <summary>
        /// 在发生异常时调用。
        /// </summary>
        /// <param name="filterContext">筛选器上下文。</param>
        public override void OnException(ExceptionContext filterContext)
        {
            //记录异常信息

            base.OnException(filterContext);
        }
    }
}
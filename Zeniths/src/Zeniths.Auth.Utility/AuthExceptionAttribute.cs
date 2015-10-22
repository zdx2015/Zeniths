using System.Web.Mvc;
using Zeniths.Auth.Service;
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
            var exEntity = AuthHelper.BuildExceptionEntity(filterContext.Exception);
            if (filterContext.HttpContext.Request.Url != null)
            {
                exEntity.Details += $"<br> Url:{filterContext.HttpContext.Request.Url.PathAndQuery}";
            }
            var service = new SystemExceptionService();
            service.Save(exEntity);

            base.OnException(filterContext);
        }
    }
}
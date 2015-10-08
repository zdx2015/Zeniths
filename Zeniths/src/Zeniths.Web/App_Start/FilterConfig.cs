using System.Web.Mvc;
using Zeniths.MvcUtility;

namespace Zeniths.Web
{
    /// <summary>
    /// 过滤器配置
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// 注册全局过滤器
        /// </summary>
        /// <param name="filters">全局过滤器集合</param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new JsonExceptionAttribute());
        }
    }
}
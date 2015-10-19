using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Zeniths.Auth.Entity;
using Zeniths.Auth.Service;
using Zeniths.Extensions;
using Zeniths.Helper;

namespace Zeniths.Auth.Utility
{
    public static class HtmlExtensions
    {
        /// <summary>
        /// 以标签方式显示系统文档标签
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="tags">文档标签</param>
        /// <returns></returns>
        public static MvcHtmlString ShowSystemDocTags(this HtmlHelper helper, string tags)
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(tags))
            {
                var sz = StringHelper.ConvertStringToArray(tags);
                foreach (var item in sz)
                {
                    sb.AppendLine($"<span class=\"label label-success\">{item}</span>");
                }
            }
            return MvcHtmlString.Create(sb.ToString());
        }

        /// <summary>
        /// 获取角色分类下拉选项
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="selected">选中的值</param>
        /// <returns></returns>
        public static MvcHtmlString RoleCategoryOptions(this HtmlHelper helper,string selected = null)
        {
            var options = AuthHelper.DictionaryOptions("RoleCategory", selected);
            return MvcHtmlString.Create(options);
        }

        /// <summary>
        /// 获取参数分类下拉选项
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="selected">选中的值</param>
        /// <returns></returns>
        public static MvcHtmlString ParamCategoryOptions(this HtmlHelper helper, string selected = null)
        {
            var options = AuthHelper.DictionaryOptions("ParamCategory", selected);
            return MvcHtmlString.Create(options);
        }
    }
}

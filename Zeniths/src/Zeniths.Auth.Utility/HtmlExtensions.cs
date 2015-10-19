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
        /// 获取系统文档下拉选项
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="selected">选中的值</param>
        /// <returns></returns>
        public static MvcHtmlString SystemDocCategory(this HtmlHelper helper, string selected = null)
        {
            var options = AuthHelper.DictionaryOptions("SystemDocCategory", selected);
            return MvcHtmlString.Create(options);
        }
    }
}

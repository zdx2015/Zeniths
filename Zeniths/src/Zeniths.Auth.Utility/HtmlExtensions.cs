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
        /// 获取数据字典下拉选项
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="dicCode">数据字典编码</param>
        /// <param name="selected">选中的值</param>
        /// <returns></returns>
        public static MvcHtmlString DictionaryOptions(this HtmlHelper helper,string dicCode, string selected = null)
        {
            var options = AuthHelper.DictionaryOptions(dicCode, selected);
            return MvcHtmlString.Create(options);
        }
    }
}

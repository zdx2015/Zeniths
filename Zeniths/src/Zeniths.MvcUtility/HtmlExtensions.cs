// ========================================================================

using System.Collections;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using Zeniths.Collections;
using Zeniths.Extensions;
using Zeniths.Helper;

namespace Zeniths.MvcUtility
{
    public static class HtmlExtensions
    {
        //private readonly static Regex HrefRegex = new Regex("\"[^\"]*\"", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        /// <summary>
        /// 生成分页
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="source">数据源</param>
        public static MvcHtmlString RenderPages<T>(this HtmlHelper helper, PageList<T> source)
        {
            if (source == null || source.Count <= 0)
            {
                return MvcHtmlString.Empty;
            }

            var pagetemplate = @"
<div class=""row paginateArea"">
    <div class=""col-sm-6"">
        <div class=""dataTables_info"">{0}</div>
    </div>
    <div class=""col-sm-6"">
        <div class=""dataTables_paginate paging_bootstrap"">
            <ul class=""pagination""  style=""margin-right:0"">
                {1}
            </ul>
        </div>
    </div>
</div>";
            int showPages = 10;
            int pageIndex = source.PageIndex;
            int totalPages = source.TotalPages;

            string pagedInfo = (source.RecordEndIndex == 0 && source.RecordEndIndex == 0) ?
                $"共 {source.TotalCount} 条" :
                $"共 {source.TotalCount} 条 当前显示 {source.RecordStartIndex} 到 {source.RecordEndIndex} 条";

            if (totalPages <= showPages)
            {
                return MvcHtmlString.Create(string.Format(pagetemplate, pagedInfo, BuildPage(1, totalPages, pageIndex)));
            }

            var pages = new StringBuilder();

            if (pageIndex <= 7)
            {
                pages.Append(BuildPage(1, 11, pageIndex));
                pages.Append("<li class=\"disabled\"><span>···</span></li>");
                pages.Append(BuildPage(totalPages, totalPages, pageIndex));
            }
            else if (pageIndex >= (totalPages - 7))
            {
                pages.Append(BuildPage(1, 1, pageIndex));
                pages.Append("<li class=\"disabled\"><span>···</span></li>");
                pages.Append(BuildPage(totalPages - 10, totalPages, pageIndex));
            }
            else
            {
                pages.Append(BuildPage(1, 1, pageIndex));
                pages.Append("<li class=\"disabled\"><span>···</span></li>");
                pages.Append(BuildPage(pageIndex - 5, pageIndex + 5, pageIndex));
                pages.Append("<li class=\"disabled\"><span>···</span></li>");
                pages.Append(BuildPage(totalPages, totalPages, pageIndex));
            }

            return MvcHtmlString.Create(string.Format(pagetemplate, pagedInfo, pages));
        }

        /// <summary>
        /// 生成页面链接
        /// </summary>
        private static string BuildPage(int start, int end, int currentPageIndex)
        {
            var sb = new StringBuilder();
            for (int i = start; i <= end; i++)
            {
                sb.Append(currentPageIndex == i
                    ? $"<li class=\"active\" title=\"第{i}页\"><span>{i}</span></li>"
                    : $"<li title=\"第{i}页\"><a data-page=\"{i}\">{i}</a></li>");
            }
            return sb.ToString();
        }

        /// <summary>
        /// 生成排序图标
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="fieldName">字段名称</param>
        public static MvcHtmlString BuildOrderImage(this HtmlHelper helper, string fieldName)
        {
            var order = HttpContext.Current.Request.Params["order"].ToStringOrEmpty();
            if (!fieldName.Equals(order, System.StringComparison.OrdinalIgnoreCase))
            {
                return MvcHtmlString.Create(@"<i class=""glyphicon glyphicon-sort"" style =""opacity: 0.2"" title=""点击排序""></i>");
            }
            var dir = HttpContext.Current.Request.Params["dir"].ToStringOrEmpty();
            if (dir.Equals("desc", System.StringComparison.OrdinalIgnoreCase))
            {
                return MvcHtmlString.Create(@"<i class=""fa fa-sort-amount-desc"" title=""倒序排序""></i>");
            }
            return MvcHtmlString.Create(@"<i class=""fa fa-sort-amount-asc"" title=""升序排序""></i>");
        }


        public static RouteValueDictionary GetQueryRouteValuesNoId(this HtmlHelper helper, object addValues = null)
        {
            return WebHelper.GetRouteValues(helper.ViewContext.RouteData.Values, addValues, true, false, "id");
        }

        public static RouteValueDictionary GetQueryRouteValues(this HtmlHelper helper, object addValues = null, params string[] removeNames)
        {
            return WebHelper.GetRouteValues(helper.ViewContext.RouteData.Values, addValues, true, false, removeNames);
        }

        public static RouteValueDictionary GetFormRouteValues(this HtmlHelper helper, object addValues = null, params string[] removeNames)
        {
            return WebHelper.GetRouteValues(helper.ViewContext.RouteData.Values, addValues, false, true, removeNames);
        }

        public static RouteValueDictionary GetCurrentRouteValues(this HtmlHelper helper, object addValues = null, params string[] removeNames)
        {
            return WebHelper.GetRouteValues(helper.ViewContext.RouteData.Values, addValues, true, true, removeNames);
        }


        public static MvcHtmlString Write(this HtmlHelper helper, bool result, string trueMsg, string falseMsg = null)
        {
            if (result)
            {
                return MvcHtmlString.Create(trueMsg);
            }
            return MvcHtmlString.Create(falseMsg);
        }


        public static MvcHtmlString IsActive(this HtmlHelper helper, bool isActive)
        {
            return isActive ? MvcHtmlString.Create("active") : MvcHtmlString.Empty;
        }

        public static MvcHtmlString IsDisabled(this HtmlHelper helper, bool isDisabled)
        {
            return isDisabled ? MvcHtmlString.Create("disabled") : MvcHtmlString.Empty;
        }

        public static MvcHtmlString IsChecked(this HtmlHelper helper, bool isChecked)
        {
            return isChecked ? MvcHtmlString.Create("checked") : MvcHtmlString.Empty;
        }

        public static MvcHtmlString IsCheckedAllowNull(this HtmlHelper helper, bool? isChecked)
        {
            if (!isChecked.HasValue)
            {
                return MvcHtmlString.Empty;
            }
            else
            {
                return IsChecked(helper, isChecked.Value);
            }
        }

        public static MvcHtmlString IsSelected(this HtmlHelper helper, bool isSelected)
        {
            return isSelected ? MvcHtmlString.Create("selected") : MvcHtmlString.Empty;
        }

        public static MvcHtmlString IsReadonly(this HtmlHelper helper, bool isReadonly)
        {
            return isReadonly ? MvcHtmlString.Create("readonly") : MvcHtmlString.Empty;
        }

        public static MvcHtmlString BoolFaIcon(this HtmlHelper helper, bool result)
        {
            if (result)
            {
                return MvcHtmlString.Create("<i class=\"fa fa-lg fa-check-square-o\" style=\"color: green\"></i>");
            }
            return MvcHtmlString.Create("<i class=\"fa fa-lg fa-close\" style=\"color: red\"></i>");
        }

        public static MvcHtmlString BoolLabel(this HtmlHelper helper, bool result, string trueLable = "启用", string falseLable = "禁用")
        {
            if (result)
            {
                return MvcHtmlString.Create("<span class=\"label label-success\">" + trueLable + "</span>");
            }
            return MvcHtmlString.Create("<span class=\"label label-danger\">" + falseLable + "</span>");
        }

        public static MvcHtmlString BoolLabelIsAgree(this HtmlHelper helper, bool? result, string trueLable = "同意", string falseLable = "不同意")
        {
            if (!result.HasValue)
            {
                return MvcHtmlString.Create(string.Empty);
            }
            return BoolLabel(helper, result.Value, trueLable, falseLable);
        }

        public static MvcHtmlString TableRowStatusClass(this HtmlHelper helper, bool isEnabled)
        {
            if (!isEnabled)
            {
                return MvcHtmlString.Create("danger");
            }
            return MvcHtmlString.Create(string.Empty);
        }
    }
}
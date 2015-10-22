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
        /// <param name="routeDatas">路由数据</param>
        public static MvcHtmlString RenderPages<T>(this HtmlHelper helper, PageList<T> source, object routeDatas = null)
        {
            var routeData = helper.ViewContext.RouteData.Values;
            string controller = routeData["controller"].ToString();
            string action = routeData["action"].ToString();
            return RenderPages(helper, source, controller, action, routeDatas);
        }

        /// <summary>
        /// 生成分页
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="source">数据源</param>
        /// <param name="controller">控制器</param>
        /// <param name="action">过程</param>
        /// <param name="routeDatas">路由数据</param>
        public static MvcHtmlString RenderPages<T>(this HtmlHelper helper, PageList<T> source, string controller,
                                                   string action, object routeDatas)
        {
            if (source == null || source.Count <= 0)
            {
                return MvcHtmlString.Empty;
            }
            var pages = new StringBuilder();
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
            int pageIndex = source.PageIndex;
            int totalPages = source.TotalPages;
            int showPages = 7;
            int miniPages = 2;
            string pagedInfo = $"共 {source.TotalCount} 条 当前显示 {source.RecordStartIndex} 到 {source.RecordEndIndex} 条";
            if (source.RecordEndIndex == 0 && source.RecordEndIndex == 0)
            {
                pagedInfo = $"共 {source.TotalCount} 条";
            }
            if (totalPages <= showPages)
            {
                for (int i = 1; i <= totalPages; i++)
                {
                    BuildPage(helper, pages, pageIndex, i, controller, action, routeDatas);
                }
                return MvcHtmlString.Create(string.Format(pagetemplate, pagedInfo, pages));
            }

            var lastShowIndex = totalPages - showPages;
            var isBuildFirstShowPages = pageIndex < showPages;
            var isBuildLastShowPages = lastShowIndex > 0 && pageIndex > lastShowIndex;
            var isBuildCenterPages = !isBuildFirstShowPages && !isBuildLastShowPages;

            #region 开始

            if (isBuildFirstShowPages)
            {
                for (int i = 1; i <= showPages; i++)
                {
                    BuildPage(helper, pages, pageIndex, i, controller, action, routeDatas);
                }
            }
            else
            {
                for (int i = 1; i <= miniPages; i++)
                {
                    BuildPage(helper, pages, pageIndex, i, controller, action, routeDatas);
                }
                pages.AppendFormat(@"<li class=""disabled""><span>...</span></li>");
            }

            #endregion

            #region 中间

            if (isBuildCenterPages)
            {
                int start = pageIndex - 3;
                int end = pageIndex + 3;
                for (int i = start; i <= end; i++)
                {
                    BuildPage(helper, pages, pageIndex, i, controller, action, routeDatas);
                }
            }

            #endregion

            #region 最后

            if (isBuildLastShowPages)
            {
                for (int i = lastShowIndex; i <= totalPages; i++)
                {
                    BuildPage(helper, pages, pageIndex, i, controller, action, routeDatas);
                }
            }
            else
            {
                pages.AppendFormat(@"<li class=""disabled""><span>...</span></li>");
                for (int i = totalPages - miniPages + 1; i <= totalPages; i++)
                {
                    BuildPage(helper, pages, pageIndex, i, controller, action, routeDatas);
                }
            }

            #endregion

            return MvcHtmlString.Create(string.Format(pagetemplate, pagedInfo, pages));
        }

        /// <summary>
        /// 生成页面
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="pages"></param>
        /// <param name="pageIndex"></param>
        /// <param name="currentPage"></param>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <param name="routeDatas">路由数据</param>
        private static void BuildPage(HtmlHelper helper, StringBuilder pages, int pageIndex, int currentPage,
                                      string controller, string action, object routeDatas)
        {
            var routeData = WebHelper.GetRouteValues(helper.ViewContext.RouteData.Values, null, true, true);

            routeData["page"] = currentPage;
            routeData["controller"] = controller;
            routeData["action"] = action;

            if (routeDatas != null)
            {
                var pros = TypeDescriptor.GetProperties(routeDatas);
                foreach (PropertyDescriptor propertyDescriptor in pros)
                {
                    routeData[propertyDescriptor.Name] = propertyDescriptor.GetValue(routeDatas);
                }
            }

            pages.AppendFormat(
                currentPage == pageIndex
                    ? @"<li class=""active"" title=""第{0}页""><span>{0}</span></li>"
                    : @"<li title=""第{0}页""><a data-page=""{0}"">{0}</a></li>", currentPage);
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

        public static MvcHtmlString IsSelected(this HtmlHelper helper, bool isSelected)
        {
            return isSelected ? MvcHtmlString.Create("selected") : MvcHtmlString.Empty;
        }

        public static MvcHtmlString IsReadonly(this HtmlHelper helper, bool isReadonly)
        {
            return isReadonly ? MvcHtmlString.Create("readonly") : MvcHtmlString.Empty;
        }

        public static MvcHtmlString BoolFaIcon(this HtmlHelper helper,bool result)
        {
            if (result)
            {
                return MvcHtmlString.Create("<i class=\"fa fa-lg fa-check-square-o\" style=\"color: green\"></i>");
            }
            return MvcHtmlString.Create("<i class=\"fa fa-lg fa-close\" style=\"color: red\"></i>");
        }

        public static MvcHtmlString BoolLabel(this HtmlHelper helper, bool result,string treeLable= "启用", string falseLable= "禁用")
        {
            if (result)
            {
                return MvcHtmlString.Create("<span class=\"label label-success\">"+ treeLable + "</span>");
            }
            return MvcHtmlString.Create("<span class=\"label label-danger\">"+ falseLable + "</span>");
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
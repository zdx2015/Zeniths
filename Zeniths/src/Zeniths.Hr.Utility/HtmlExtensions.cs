using System.Web.Mvc;
using Zeniths.Helper;
using Zeniths.Auth.Utility;
using System.Web;

namespace Zeniths.Hr.Utility
{
    /// <summary>
    /// 工作流下拉选项类
    /// </summary>
    public static class HtmlExtensions
    {
        ///// <summary>
        ///// 获取流程表单下拉选项
        ///// </summary>
        ///// <param name="helper"></param>
        ///// <param name="selected">选中的值</param>
        ///// <returns></returns>
        //public static MvcHtmlString WorkFlowFormCategoryOptions(this HtmlHelper helper, string selected = null)
        //{
        //    var options = AuthHelper.DictionaryOptions("WorkFlow", selected);
        //    return MvcHtmlString.Create(options);
        //}

        /// <summary>
        /// 获取费用类型明细下拉选项
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="selected">选中的值</param>
        /// <returns></returns>
        public static MvcHtmlString DailyReimburseDetailCategoryOptions(this HtmlHelper helper,  string selected = null)
        {
            
            return MvcHtmlString.Create(AuthHelper.BuildDicListOptions("DailyReimburseCategory", selected));
        }

        /// <summary>
        /// 获取费用类型下拉选项
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="selected">选中的值</param>
        /// <returns></returns>
        public static MvcHtmlString DailyReimburseDicCategoryOptions(this HtmlHelper helper, string selected = null)
        {

            return MvcHtmlString.Create(AuthHelper.BuildDicOptions("DailyReimburseCategory", selected));
        }

        /// <summary>
        /// 获取请假类别选项
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="selected"></param>
        /// <returns></returns>
        public static MvcHtmlString LeaveCategoryOptitions(this HtmlHelper helper,string controlName, string selected=null)
        {
            return MvcHtmlString.Create(AuthHelper.BuildDicRadioBoxList("LeaveCategory", controlName, selected));
        }


    }
}
using System;
using System.Text;
using System.Web;
using Zeniths.Auth.Entity;
using Zeniths.Auth.Service;
using Zeniths.Extensions;
using Zeniths.Helper;

namespace Zeniths.Auth.Utility
{
    public static class AuthHelper
    {
        /// <summary>
        /// 生成数据字典下拉选项
        /// </summary>
        /// <param name="dicCode">数据字典编码</param>
        /// <param name="selected">选中的值</param>
        /// <returns></returns>
        public static string BuildDicOptions(string dicCode, string selected = null)
        {
            if (string.IsNullOrEmpty(dicCode))
            {
                throw new ArgumentNullException(nameof(dicCode), "数据字典编码不允许为空");
            }
            var service = new SystemDictionaryService();
            var detailsList = service.GetEnabledDetailsListByDicCode(dicCode);
            var options = new StringBuilder();
            foreach (SystemDictionaryDetails item in detailsList)
            {
                string name = item.Name;
                string value = item.Value.ToStringOrEmpty();
                string display = $"{StringHelper.GetFirstAlpha(name).ToUpper()}:{name}";
                options.AppendFormat("<option value=\"{0}\"{1}>{2}</option>", value,
                    value.Equals(selected.ToStringOrEmpty()) ? " selected" : string.Empty, display);
            }
            return options.ToString();
        }


        /// <summary>
        /// 生成数据字典下拉选项
        /// </summary>
        /// <param name="dicCode">数据字典编码</param>
        /// <param name="selected">选中的值</param>
        /// <returns></returns>
        public static string BuildDicListOptions(string dicCode,string selected = null)
        {
            if (string.IsNullOrEmpty(dicCode))
            {
                throw new ArgumentNullException(nameof(dicCode), "数据字典编码不允许为空");
            }
            var service = new SystemDictionaryService();
            var detailsList = service.GetEnabledDicListByDicCode(dicCode);
            var options = WebHelper.GetSelectGroupOptions(detailsList,"Name","Id","Category",selected);
            return options.ToString();
        }


        /// <summary>
        /// 生成复选框组
        /// </summary>
        /// <param name="dicCode">字典编码</param>
        /// <param name="controlName">控件名称</param>
        /// <param name="selected">选中的值</param>
        /// <returns></returns>
        public static string BuildDicCheckBoxList(string dicCode, string controlName, string selected = null)
        {
            return BuildDicBoxList(dicCode, controlName, true, selected);
        }

        /// <summary>
        /// 生成单选框组
        /// </summary>
        /// <param name="dicCode">字典编码</param>
        /// <param name="controlName">控件名称</param>
        /// <param name="selected">选中的值</param>
        /// <returns></returns>
        public static string BuildDicRadioBoxList(string dicCode, string controlName, string selected = null)
        {
            return BuildDicBoxList(dicCode, controlName, false, selected);
        }

        private static string BuildDicBoxList(string dicCode, string controlName, bool isCheckbox, string selected = null)
        {
            if (string.IsNullOrEmpty(dicCode))
            {
                throw new ArgumentNullException(nameof(dicCode), "数据字典编码不允许为空");
            }
            var service = new SystemDictionaryService();
            var detailsList = service.GetEnabledDetailsListByDicCode(dicCode);
            var options = new StringBuilder();
            string listClass = "radio-list";
            string inlineClass = "radio-inline";
            string controlClass = "iradiobox-control";
            string controlType = "radio";
            if (isCheckbox)
            {
                listClass = "checkbox-list";
                inlineClass = "checkbox-inline";
                controlClass = "icheckbox-control";
                controlType = "checkbox";
            }

            options.AppendFormat($"<div class=\"{listClass}\">");

            foreach (SystemDictionaryDetails item in detailsList)
            {
                string name = item.Name;
                string value = item.Value.ToStringOrEmpty();
                bool isChecked = selected.ToStringOrEmpty().Equals(value.ToStringOrEmpty());
                string _checked = isChecked ? "checked" : string.Empty;
                options.AppendFormat($"<label class=\"{inlineClass}\">");
                options.AppendFormat($"<input class=\"{controlClass}\" name=\"{controlName}\" value=\"{value}\" type=\"{controlType}\" {_checked} />");
                options.AppendFormat($"<span>{name}</span>");
                options.AppendFormat("</label>");
            }
            options.Append("</div>");
            return options.ToString();
        }



        /// <summary>
        /// 生成异常对象
        /// </summary>
        /// <param name="ex">异常对象</param>
        public static Zeniths.Auth.Entity.SystemException BuildExceptionEntity(Exception ex)
        {
            return new Zeniths.Auth.Entity.SystemException
            {
                Message = ex.Message,
                Details = StringHelper.BuildExceptionDetails(ex),
                IPAddress = WebHelper.GetClientIP(),
                CreateDateTime = DateTime.Now
            };
        }

    }
}
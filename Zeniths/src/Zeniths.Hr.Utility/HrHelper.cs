using System;
using System.Text;
using System.Web;
using Zeniths.Hr.Entity;
using Zeniths.Hr.Service;
using Zeniths.Extensions;
using Zeniths.Helper;

namespace Zeniths.Hr.Utility
{
    public static class HrHelper
    {
        /// <summary>
        /// 生成员工下拉选项
        /// </summary>
        /// <returns></returns>
        public static string BuildDicListOptions(string selected = null)
        {
            var service = new EmployeeService();
            var empList = service.GetList();
            var options = WebHelper.GetSelectGroupOptions(empList, "Name", "UserId", "Department", selected);
            return options.ToString();
        }
    }
}

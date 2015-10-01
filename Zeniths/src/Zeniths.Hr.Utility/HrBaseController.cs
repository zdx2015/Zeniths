using Zeniths.Extensions;
using Zeniths.Helper;
using Zeniths.MvcUtility;

namespace Zeniths.Hr.Utility
{
    public class HrBaseController: JsonController
    {

        public int GetPageIndex()
        {
            return WebHelper.GetFormString("page").ToInt(1);
        }

        public int GetPageSize()
        {
            return 10;
        }

        public string GetOrderName()
        {
            return WebHelper.GetFormString("order");
        }

        public string GetOrderDir()
        {
            return WebHelper.GetFormString("dir");
        }
    }
}
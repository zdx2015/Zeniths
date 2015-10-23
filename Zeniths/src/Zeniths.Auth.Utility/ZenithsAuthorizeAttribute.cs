using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Zeniths.Auth.Utility
{
    public class ZenithsAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!httpContext.User.Identity.IsAuthenticated)
            {
                httpContext.Response.Redirect(FormsAuthentication.LoginUrl);
            }
            return base.AuthorizeCore(httpContext);
        }
    }
}

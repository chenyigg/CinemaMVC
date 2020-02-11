using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class MyFilterAttr : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //系统自定义的过滤方法
            //base.OnAuthorization(filterContext);

            if (filterContext.HttpContext.Session["UserAccount"] == null)
            {
                filterContext.Result = new RedirectResult("/Login/Check");
            }
        }
    }
}
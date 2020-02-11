using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL;
using Model;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class UrlController : Controller
    {
        // GET: Url
        /// <summary>
        /// 一旦访问就进行数据更改，实现模拟支付
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //UrlCountInnfo ui = new UrlPageInfoBLL().Select(u => true).Cast<UrlCountInnfo>().FirstOrDefault();
            //ui.Msg++;
            int b = new UrlPageInfoBLL().UpdateNow();
            return View();
        }
    }
}
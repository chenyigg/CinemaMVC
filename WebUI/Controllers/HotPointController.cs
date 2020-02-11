using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class HotPointController : Controller
    {
        // GET: HotPoint
        /// <summary>
        /// 初始化热点新闻界面
        /// </summary>
        /// <returns></returns>
        public ActionResult PointLoad()
        {
            ViewBag.News = new NewsInfoBLL().Select(u => true);
            return View();
        }
    }
}
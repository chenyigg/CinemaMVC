using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class CinemaController : Controller
    {
        // GET: Cinema
        public ActionResult AllCinema()
        {
            return View();
        }

        /// <summary>
        /// 筛选
        /// </summary>
        /// <returns></returns>
        public ActionResult Filtrate()
        {
            List<CinemaInfo> ls = new CinemaInfoBLL().Filtrate(Request["CinemaArea"].Replace("\n", "").Replace(" ", ""), Request["OfficeName"].Replace("\n", "").Replace("\t", "").Replace(" ", ""));
            return Json(ls);
        }

        /// <summary>
        /// 查找厅名
        /// </summary>
        /// <returns></returns>
        public ActionResult FindOfficeName()
        {
            List<OfficeInfo> ls = new OfficeInfoBLL().Select(u => true).Cast<OfficeInfo>().ToList().GroupBy(p => p.OfficeName)
                                  .Select(g => g.FirstOrDefault())
                                  .ToList();

            return Json(ls);
        }

        /// <summary>
        /// 查找电影院
        /// </summary>
        /// <returns></returns>
        public ActionResult FindCinema()
        {
            List<CinemaInfo> ls = new CinemaInfoBLL().Select(u => true).Cast<CinemaInfo>().ToList();
            return Json(ls);
        }
    }
}
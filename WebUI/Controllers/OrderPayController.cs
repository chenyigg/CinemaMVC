using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using BLL;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace WebUI.Controllers
{
    public class OrderPayController : Controller
    {
        public static int Count = 0;
        public static int p = 0;

        // GET: OrderPay
        public ActionResult PayLoad()
        {
            dynamic or = null;
            if (Request["OrderID"] != null)
            {
                dynamic od = LoadInfo(Convert.ToInt32(Request["OrderID"]));
                or = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(od));
            }
            if (p == 0)
            {
                //打开订单界面前，先获取支付地址栏的访问次数
                Count = new UrlPageInfoBLL().Select(u => true).Cast<UrlCountInnfo>().FirstOrDefault().Msg;
            }
            p++;
            return View(or);
        }

        public ActionResult UrlAbout()
        {
            return View();
        }

        public dynamic LoadInfo(int OrderID)
        {
            return new OrderInfoBLL().LoadInfo(OrderID);
        }

        public ActionResult IsPay()
        {
            int x = Convert.ToInt32(Request["OI"]);
            int k = new UrlPageInfoBLL().Select(u => true).Cast<UrlCountInnfo>().FirstOrDefault().Msg;

            if (k != Count)
            {
                Count = k;

                OrderInfo op = new OrderInfoBLL().Select(u => u.OrderID == x).Cast<OrderInfo>().FirstOrDefault();
                op.IsPay = 1;

                int ppp = new OrderInfoBLL().Update(op);

                if (ppp > 0)
                {
                    return Json(new { state = "ok" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { state = "no" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { state = "no" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
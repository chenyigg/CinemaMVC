using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL;
using Model;
using System.Web.Script.Serialization;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class ChooseSeatController : Controller
    {
        // GET: ChooseSeat
        public ActionResult SeatLoad()
        {
            return View();
        }

        /// <summary>
        /// 电影信息
        /// </summary>
        /// <returns></returns>
        public ActionResult LoadMovieInfo()
        {
            int ChipInfoID = Convert.ToInt32(Request["ChipInfoID"].ToString());
            string MovieName = new ChipInfoBLL().Select(u => u.ChipInfoID == ChipInfoID).Cast<ChipInfo>().FirstOrDefault().MovieName;
            MovieInfo mv = new MovieInfoBLL().Select(d => d.MovieName == MovieName).Cast<MovieInfo>().FirstOrDefault();
            return Json(mv);
        }

        /// <summary>
        /// 座位信息
        /// </summary>
        /// <returns></returns>
        public ActionResult LoadSeatInfo()
        {
            int officeID = Convert.ToInt32(Request["OfficeID"].ToString());
            int ChipInfoID = Convert.ToInt32(Request["ChipInfoID"].ToString());
            List<SeatInfo> ls = new SeatInfoBLL().Select(u => u.OfficeID == officeID && u.ChipInfoID == ChipInfoID).Cast<SeatInfo>().ToList();
            return Json(ls);
        }

        /// <summary>
        /// 影院信息
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ActionResult LoadCinema()
        {
            int ChipInfoID = Convert.ToInt32(Request["ChipInfoID"]);
            int CinemaID = new ChipInfoBLL().Select(u => u.ChipInfoID == ChipInfoID).Cast<ChipInfo>().FirstOrDefault().CinemaID;
            CinemaInfo ci = new CinemaInfoBLL().Select(u => u.CinemaID == CinemaID).Cast<CinemaInfo>().FirstOrDefault();

            //遇到循环引用，优先使用匿名类
            //或者导入Newtonsoft.Json工具包来进行序列化
            var cm = new { ci.CinemaAddress, ci.CinemaArea, ci.CinemaID, ci.CinemaImg, ci.CinemaName };
            return Json(cm, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 厅院信息
        /// </summary>
        /// <returns></returns>
        public ActionResult LoadOffice()
        {
            int ChipInfoID = Convert.ToInt32(Request["ChipInfoID"]);
            int OfficeID = new ChipInfoBLL().Select(u => u.ChipInfoID == ChipInfoID).Cast<ChipInfo>().FirstOrDefault().OfficeID;
            OfficeInfo oi = new OfficeInfoBLL().Select(u => u.OfficeID == OfficeID).Cast<OfficeInfo>().FirstOrDefault();

            //遇到循环引用，优先使用匿名类
            //或者导入Newtonsoft.Json工具包来进行序列化
            var cm = new { oi.OfficeName, oi.OfficeID, oi.NullColOne, oi.NullColTwo, oi.OfficeCount };
            return Json(cm, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 确认选座
        /// </summary>
        /// <returns></returns>
        public ActionResult SureChooseSeat()
        {
            string[] strArray = Request["SeatIDarray"].Replace("[", string.Empty).Replace("]", string.Empty).Split(',');
            int ChipInfoID = Convert.ToInt32(Request["ChipInfoID"].ToString());
            int officeID = Convert.ToInt32(Request["officeID"].ToString());
            double money = Convert.ToDouble(Request["Money"].ToString());

            //确定选座方法
            int a = 0;
            List<SeatInfo> ls = new List<SeatInfo>();
            for (int i = 0; i < strArray.Length; i++)
            {
                int SeatID = Convert.ToInt32(strArray[i]);
                ls.Add(new SeatInfoBLL().Select(u => u.ChipInfoID == ChipInfoID && u.SeatID == SeatID).Cast<SeatInfo>().FirstOrDefault());
            }

            for (int i = 0; i < ls.Count; i++)
            {
                ls[i].IsSeat = 2;
                a += new SeatInfoBLL().Update(ls[i]);
            }
            a = a == ls.Count ? 1 : 0;

            //生成订单方法
            string orderID = "";
            OrderInfo orderInfoModel = new OrderInfo();
            orderInfoModel.OrderSumMoney = Convert.ToDecimal(strArray.Length * Convert.ToDouble(Request["Money"]));
            orderInfoModel.IsPay = 0;
            orderInfoModel.PayTime = 900;
            orderInfoModel.OfficeID = Convert.ToInt32(officeID);
            string UserAccount = Session["UserAccount"].ToString();
            orderInfoModel.UsersID = new UsersInfoBLL().Select(u => u.UserAccount == UserAccount).Cast<UsersInfo>().FirstOrDefault().UsersID;
            orderInfoModel.ChipInfoID = Convert.ToInt32(ChipInfoID);
            orderInfoModel.OrderTime = DateTime.Now;
            orderInfoModel.MovieName = new ChipInfoBLL().Select(u => u.ChipInfoID == ChipInfoID).Cast<ChipInfo>().FirstOrDefault().MovieName;
            int CinemaID = new ChipInfoBLL().Select(u => u.ChipInfoID == ChipInfoID).Cast<ChipInfo>().FirstOrDefault().CinemaID;
            orderInfoModel.CinemaAddress = new CinemaInfoBLL().Select(u => u.CinemaID == CinemaID).Cast<CinemaInfo>().FirstOrDefault().CinemaAddress;
            int b = new OrderInfoBLL().Add(orderInfoModel);

            //生成订单详情方法
            OrderDetails orderDetailsModel = new OrderDetails();
            orderDetailsModel.OrderID = Convert.ToInt32(orderInfoModel.OrderID);
            orderDetailsModel.OrderDetailsPrice = Convert.ToDecimal(money);
            orderDetailsModel.StartTime = new ChipInfoBLL().Select(u => u.ChipInfoID == ChipInfoID).Cast<ChipInfo>().FirstOrDefault().StartTime;
            orderDetailsModel.StopTime = new ChipInfoBLL().Select(u => u.ChipInfoID == ChipInfoID).Cast<ChipInfo>().FirstOrDefault().StopTime;

            int c = 0;
            for (int i = 0; i < strArray.Length; i++)
            {
                orderDetailsModel.SeatID = Convert.ToInt32(strArray[i]);
                c += new OrderDetailsBLL().Add(orderDetailsModel);
            }
            c = c == strArray.Length ? 1 : 0;

            //判断座位表，订单表，订单详情表是否都插入成功！
            if (c == 1 && b == 1 && a == 1)
            {
                return Json(new { orderInfoModel.OrderID });
            }
            else
            {
                return Json(new { OrderID = 0 });
            }
        }
    }
}
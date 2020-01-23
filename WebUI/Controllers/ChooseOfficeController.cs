using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Newtonsoft.Json;

namespace WebUI.Controllers
{
    public class ChooseOfficeController : Controller
    {
        // GET: ChooseOffice
        public ActionResult OfficeLoad(CinemaInfo ci, MovieInfo mi)
        {
            CinemaInfo om = new CinemaInfoBLL().Select(u => u.CinemaID == ci.CinemaID).Cast<CinemaInfo>().ToList().FirstOrDefault();
            ViewBag.MovieInfo = new OfficeInfoBLL().FindMovieInfo(ci, mi);
            return View(om);
        }

        //public void FindTod()
        //{
        //    string b = Request["MovieName"].ToString();
        //    MovieInfo mi = new MovieInfo();
        //    mi.MovieName = Request["MovieName"].ToString();
        //    CinemaInfo ci = new CinemaInfo();
        //    ci.CinemaID = Convert.ToInt32(Request["CinemaID"]);

        //    List<ShowDetails> sd = new OfficeInfoBLL().FindTod(mi, ci);
        //    var jsondata = JsonConvert.SerializeObject(sd);
        //    Response.Write(jsondata);
        //    Response.End();
        //}

        //public void FindTom()
        //{
        //    string b = Request["MovieName"].ToString();
        //    MovieInfoModel mi = new MovieInfoModel();
        //    mi.MovieName = Request["MovieName"].ToString();
        //    CinemaInfoModel ci = new CinemaInfoModel();
        //    ci.CinemaID = Convert.ToInt32(Request["CinemaID"]);

        //    List<ShowDetails> sd = new ChooseOfficeBLL().FindTom(mi, ci);
        //    var jsondata = JsonConvert.SerializeObject(sd);
        //    Response.Write(jsondata);
        //    Response.End();
        //}

        /// <summary>
        /// 找电影信息
        /// </summary>
        public void FindMovieInfo()
        {
            //如果传入的影片ID和影院ID都不为空，
            //证明从电影界面点击进来
            if (!String.IsNullOrEmpty(Request["CinemaID"]) && !String.IsNullOrEmpty(Request["MovieID"]))
            {
                MovieInfo mi = new MovieInfo();
                mi.MovieID = Convert.ToInt32(Request["MovieID"]);
                CinemaInfo ci = new CinemaInfo();
                ci.CinemaID = Convert.ToInt32(Request["CinemaID"]);

                List<MovieInfo> sd = new OfficeInfoBLL().FindMovieInfo(ci, mi);
                var jsondata = JsonConvert.SerializeObject(sd);
                Response.Write(jsondata);
                Response.End();
            }
            else
            {
                MovieInfo mi = new MovieInfo();
                mi.MovieID = 0;
                CinemaInfo ci = new CinemaInfo();
                ci.CinemaID = Convert.ToInt32(Request["CinemaID"]);

                List<MovieInfo> sd = new OfficeInfoBLL().FindMovieInfo(ci, mi);
                var jsondata = JsonConvert.SerializeObject(sd);
                Response.Write(jsondata);
                Response.End();
            }
        }

        /// <summary>
        /// 找厅位排片
        /// </summary>
        /// <returns></returns>
        public string FindOfficeInfo()
        {
            //如果传入的影片ID和影院ID都不为空，
            //证明从电影详情界面点击进来
            if (!String.IsNullOrEmpty(Request["CinemaID"]) && !String.IsNullOrEmpty(Request["MovieID"]))
            {
                MovieInfo mi = new MovieInfo();
                mi.MovieID = Convert.ToInt32(Request["MovieID"]);
                CinemaInfo ci = new CinemaInfo();
                ci.CinemaID = Convert.ToInt32(Request["CinemaID"]);

                List<dynamic> ls = new OfficeInfoBLL().FindOffice(mi, ci);
                var jsondata = JsonConvert.SerializeObject(ls);
                return jsondata;
            }
            //否则是从影院界面进入
            //需要加载出该影院所有排片
            else
            {
                CinemaInfo ci = new CinemaInfo();
                ci.CinemaID = Convert.ToInt32(Request["CinemaID"]);

                List<dynamic> sd = new OfficeInfoBLL().FindOffice(ci);
                var jsondata = JsonConvert.SerializeObject(sd);
                return jsondata;
            }
        }
    }
}
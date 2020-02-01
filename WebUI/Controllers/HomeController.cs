using System;
using System.Collections.Generic;
using System.Linq;
using BLL;
using Model;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Infrastructure;
using Newtonsoft.Json;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        /// <summary>
        /// Default控制器加载时初始化界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Default()
        {
            //生成正在热映电影信息
            SelectMovieInfo();

            //生成今日票房信息
            SelectMovieMoneyTop();

            //生成最受期待
            SelectScoreMovieInfo();

            //生成经典Top
            SelectMovieInfos();

            //轮播图片加载
            lunboImgLoad();

            return View();
        }

        #region 生成正在热映电影信息

        public void SelectMovieInfo()
        {
            int count;
            ViewBag.Hot = new MovieInfoBLL().Select(m => m.MovieReleaseDate < DateTime.Now);
            ViewBag.HotCount = new MovieInfoBLL().Select(m => m.MovieReleaseDate < DateTime.Now).Cast<MovieInfo>().Count();

            ViewBag.NoUp = new MovieInfoBLL().Select(m => m.MovieReleaseDate > DateTime.Now);
            ViewBag.NoUpCount = new MovieInfoBLL().Select(m => m.MovieReleaseDate > DateTime.Now).Cast<MovieInfo>().Count();

            //ViewBag.Score = new MovieInfoBLL().SelectScoreMovieInfo();
            ViewBag.Score = new MovieInfoBLL().SelectPage(1, 10, out count, m => true, m => m.MovieScore, false);
            ViewBag.ScoreCount = new MovieInfoBLL().SelectPage(1, 10, out count, m => true, m => m.MovieScore, false).Cast<MovieInfo>().Count();
        }

        #endregion 生成正在热映电影信息

        #region 生成今日票房信息

        public void SelectMovieMoneyTop()
        {
            ViewBag.MovieMoneyTop = new MovieInfoBLL().SelectMovieMoneyTopModel();
            List<object> ls = new List<object>();
            foreach (var item in ViewBag.MovieMoneyTop)
            {
                //先序列化再反序列，即可前台获取匿名对象
                ls.Add(JsonConvert.DeserializeObject(JsonConvert.SerializeObject(new { MovieID = item.MovieID, MovieName = item.MovieName, MovieCover = item.MovieCover, OrderSumMoney = item.OrderSumMoney })));
            }
            ViewBag.MovieMoneyTop = ls;
        }

        #endregion 生成今日票房信息

        #region 生成最受期待

        public void SelectScoreMovieInfo()
        {
            int count;
            ViewBag.ScoreMovieInfo = new MovieInfoBLL().SelectPage(1, 10, out count, u => true, m => m.MovieScore, false);
        }

        #endregion 生成最受期待

        #region 生成经典Top

        public void SelectMovieInfos()
        {
            int count;
            ViewBag.MovieInfos = new MovieInfoBLL().SelectPage(1, 10, out count, m => m.MovieReleaseDate < DateTime.Today, m => true, true);
        }

        #endregion 生成经典Top

        #region 轮播图片加载

        public void lunboImgLoad()
        {
            int count;
            ViewBag.LbMovieInfo = new MovieInfoBLL().SelectPage(1, 7, out count, m => true, m => m.MovieID, true);
        }

        #endregion 轮播图片加载

        #region 头像加载

        public ActionResult HasFace()
        {
            if (Session["UserAccount"] == null || Session["UserAccount"].ToString() == "" || Session["UserPwd"] == null || Session["UserPwd"].ToString() == "")
            {
                return Json(new { State = "no" });
            }
            else
            {
                UsersInfo User = new UsersInfo();
                User.UserAccount = Session["UserAccount"].ToString();
                User = new UsersInfoBLL().Select(u => u.UserAccount == User.UserAccount).Cast<UsersInfo>().FirstOrDefault();
                return Json(new { State = "ok", UserFace = User.UserFace });
            }
        }

        #endregion 头像加载
    }
}
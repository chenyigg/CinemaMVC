using BLL;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class AllMovieController : Controller
    {
        public static int pageNow; //当前页
        public static int pageCount; //页面数
        public int pageSize = 18; //页面需要摆放的数量
        public static List<MovieInfo> mi = new List<MovieInfo>();//定义一个容器

        // GET: AllMovie
        public ActionResult MovieInfo()
        {
            return View();
        }

        /// <summary>
        /// 即将上映
        /// </summary>
        /// <returns></returns>
        public string jjsy()
        {
            int Count = 0;
            //每次查询时将要页面翻页的变量重置
            pageNow = 1;

            List<MovieInfo> list = new MovieInfoBLL().SelectPage(pageNow, pageSize, out Count, m => m.MovieReleaseDate > DateTime.Now, u => u.MovieID, true).Cast<MovieInfo>().ToList();
            var jsondata = JsonConvert.SerializeObject(list);

            pageNow = mi.Count > 0 ? 1 : 0;
            pageCount = (Count % pageSize) > 0 ? (Count / pageSize) + 1 : Count / pageSize;

            return jsondata;
        }

        /// <summary>
        /// 经典电影
        /// </summary>
        /// <returns></returns>
        public string jddy()
        {
            int Count = 0;
            //每次查询时将要页面翻页的变量重置
            pageNow = 1;

            List<MovieInfo> list = new MovieInfoBLL().SelectPage(pageNow, pageSize, out Count, m => m.MovieReleaseDate < DateTime.Today, u => u.MovieID, true).Cast<MovieInfo>().ToList();
            var jsondata = JsonConvert.SerializeObject(list);

            pageNow = mi.Count > 0 ? 1 : 0;
            pageCount = (Count % pageSize) > 0 ? (Count / pageSize) + 1 : Count / pageSize;

            return jsondata;
        }

        /// <summary>
        /// 得到当前页码和总页数
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPageInfo()
        {
            return Json(new { PageNow = pageNow, PageCount = pageCount });
        }

        /// <summary>
        /// 电影集合(用于初始化)
        /// </summary>
        public ActionResult LoadMovieInfo()
        {
            string mt = Request["MovieType"];
            string ma = Request["MovieArea"];
            string my = Request["MovieYears"];

            int Count = 0;
            //每次查询时将要页面翻页的变量重置
            pageNow = 1;

            mi = new MovieInfoBLL().SelectPage(pageNow, pageSize, out Count, u => u.MovieArea.Contains(ma) &&
            u.MovieType.Contains(mt) && u.MovieYears.ToString().Contains(my),
            u => u.MovieID, true).Cast<MovieInfo>().ToList();

            pageNow = mi.Count > 0 ? 1 : 0;
            pageCount = (Count % pageSize) > 0 ? (Count / pageSize) + 1 : Count / pageSize;

            return Json(mi);
        }

        /// <summary>
        /// 首页电影
        /// </summary>
        public ActionResult FirstPageMovieInfo()
        {
            if (pageNow == 0) return null;

            string mt = Request["MovieType"];
            string ma = Request["MovieArea"];
            string my = Request["MovieYears"];

            //将页码变为1
            pageNow = 1;

            int Count = 0;

            mi = new MovieInfoBLL().SelectPage(pageNow, pageSize, out Count, u => u.MovieArea.Contains(ma) &&
            u.MovieType.Contains(mt) && u.MovieYears.ToString().Contains(my),
            u => u.MovieID, true).Cast<MovieInfo>().ToList();

            pageCount = (Count % pageSize) > 0 ? (Count / pageSize) + 1 : Count / pageSize;
            pageNow = mi.Count > 0 ? 1 : 0;

            return Json(mi);
        }

        /// <summary>
        /// 末页电影
        /// </summary>
        public ActionResult EndPageMovieInfo()
        {
            if (pageNow == 0) return null;

            string mt = Request["MovieType"];
            string ma = Request["MovieArea"];
            string my = Request["MovieYears"];

            pageNow = pageCount;

            int Count = 0;

            mi = new MovieInfoBLL().SelectPage(pageNow, pageSize, out Count, u => u.MovieArea.Contains(ma) &&
            u.MovieType.Contains(mt) && u.MovieYears.ToString().Contains(my),
            u => u.MovieID, true).Cast<MovieInfo>().ToList();

            pageCount = (Count % pageSize) > 0 ? (Count / pageSize) + 1 : Count / pageSize;
            pageNow = mi.Count > 0 ? pageCount : 0;

            return Json(mi);
        }

        /// <summary>
        /// 上一页
        /// </summary>
        public ActionResult PrevPageMovieInfo()
        {
            //如果没值或者已经是第一页，则直接返回
            if (pageNow == 0 || pageNow == 1) return null;

            string mt = Request["MovieType"];
            string ma = Request["MovieArea"];
            string my = Request["MovieYears"];

            //不然的话就返回上一页
            pageNow -= 1;

            int Count = 0;

            mi = new MovieInfoBLL().SelectPage(pageNow, pageSize, out Count, u => u.MovieArea.Contains(ma) &&
            u.MovieType.Contains(mt) && u.MovieYears.ToString().Contains(my),
            u => u.MovieID, true).Cast<MovieInfo>().ToList();

            pageNow = mi.Count > 0 ? pageNow : 0;
            pageCount = (Count % pageSize) > 0 ? (Count / pageSize) + 1 : Count / pageSize;

            return Json(mi);
        }

        /// <summary>
        /// 下一页
        /// </summary>
        public ActionResult NextPageMovieInfo()
        {
            //如果没值或者已经是最后一页，则直接返回
            if (pageNow == 0 || pageNow == pageCount) return null;

            string mt = Request["MovieType"];
            string ma = Request["MovieArea"];
            string my = Request["MovieYears"];

            //不然的话就去到下一页
            pageNow += 1;

            int Count = 0;

            mi = new MovieInfoBLL().SelectPage(pageNow, pageSize, out Count, u => u.MovieArea.Contains(ma) &&
            u.MovieType.Contains(mt) && u.MovieYears.ToString().Contains(my),
            u => u.MovieID, true).Cast<MovieInfo>().ToList();

            pageNow = mi.Count > 0 ? pageNow : 0;
            pageCount = (Count % pageSize) > 0 ? (Count / pageSize) + 1 : Count / pageSize;

            return Json(mi);
        }
    }
}
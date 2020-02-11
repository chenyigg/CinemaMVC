using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class ChooseCinemaController : Controller
    {
        // GET: ChooseCinema
        public ActionResult CinemaLoad(int MovieID)
        {
            SelectMovieInfo(MovieID);

            ChooseCinemaInfo(MovieID);

            return View();
        }

        /// <summary>
        /// 通过电影ID选择电影院
        /// </summary>
        /// <param name="movieID"></param>
        public void ChooseCinemaInfo(int movieID)
        {
            var MovieID = Convert.ToInt32(movieID);
            //找到所有正在播放此电影的影院
            MovieInfo model = new MovieInfo();
            model.MovieID = MovieID;
            ViewBag.ChooseCinemaInfo = new CinemaInfoBLL().ChooseCinemaInfo(model);
            //List<CinemaInfo> ls = new CinemaInfoBLL().ChooseCinemaInfo(model).ca;
        }

        /// <summary>
        /// 通过电影ID查询电影信息
        /// </summary>
        /// <param name="MovieID"></param>
        public void SelectMovieInfo(int MovieID)
        {
            MovieInfo mi = new MovieInfo();
            mi.MovieID = Convert.ToInt32(MovieID);
            ViewBag.MovieInfo = new MovieInfoBLL().Select(m => m.MovieID == MovieID);
        }
    }
}
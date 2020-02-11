using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class ListController : Controller
    {
        // GET: List
        public ActionResult ListLoad()
        {
            SelectMovieInfo1();

            SelectMovieInfo2();

            SelectMovieInfo3();

            SelectMovieInfo4();

            return View();
        }

        private void SelectMovieInfo1()
        {
            int count;
            ViewBag.MovieInfo1 = new MovieInfoBLL().SelectPage(1, 10, out count, u => true, u => u.MovieScore, false);
        }

        private void SelectMovieInfo2()
        {
            int count;
            ViewBag.MovieInfo2 = new MovieInfoBLL().SelectPage(1, 10, out count, u => true, u => !u.MovieArea.Contains("大陆") || !u.MovieArea.Contains("中国香港"), false);
        }

        private void SelectMovieInfo3()
        {
            int count;
            ViewBag.MovieInfo3 = new MovieInfoBLL().SelectPage(1, 10, out count, u => true, u => u.MovieType, false);
        }

        private void SelectMovieInfo4()
        {
            int count;
            ViewBag.MovieInfo4 = new MovieInfoBLL().SelectPage(1, 10, out count, u => true, u => !u.MovieArea.Contains("大陆") || !u.MovieArea.Contains("中国香港"), false);
        }
    }
}
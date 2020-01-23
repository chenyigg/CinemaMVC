using BLL;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class DetailsController : Controller
    {
        public static int pageNow; //当前页
        public static int pageCount; //页面数
        public int pageSize = 5; //页面需要摆放的数量

        //电影信息
        public MovieInfo mz = new MovieInfo();

        // GET: Details
        /// <summary>
        /// 初始化电影详情界面
        /// </summary>
        /// <returns></returns>
        public ActionResult MovieDetails()
        {
            #region 状态校验

            if (Request.UrlReferrer == null)
            {
                Response.Redirect("Default.aspx");
            }

            #endregion 状态校验

            List<MovieInfo> ls = new List<MovieInfo>();
            if (Request["MovieID"] != null)
            {
                //LINQ表达式内部无法识别C#方法，需要在外部转好
                var MovieID = Convert.ToInt32(Request["MovieID"]);
                ls = new MovieInfoBLL().Select(m => m.MovieID == MovieID).Cast<MovieInfo>().ToList();
            }

            return View(ls[0]);
        }

        /// <summary>
        /// 得到当前页码和总页数
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPageInfo()
        {
            return Json(new { pageNow, pageCount });
        }

        /// <summary>
        /// 电影评论
        /// </summary>
        public string SelectCommentInfo()
        {
            int Count = 0;
            //每次查询时将要页面翻页的变量重置
            pageNow = 1;
            var MovieID = Convert.ToInt32(Request["MovieID"]);

            List<CommentInfo> ls = new CommentInfoBLL().SelectPage(pageNow, pageSize, out Count, u => u.MovieID == MovieID, u => u.CommentID, false).Cast<CommentInfo>().ToList();

            ls = new CommentInfoBLL().Fitle(ls);

            pageNow = ls.Count > 0 ? 1 : 0;
            pageCount = (Count % pageSize) > 0 ? (Count / pageSize) + 1 : Count / pageSize;

            //.Net自带的Json
            //JavaScriptSerializer js = new JavaScriptSerializer();
            //string json = js.Serialize(ls);

            return JsonConvert.SerializeObject(ls);
        }

        /// <summary>
        /// 发表评论
        /// </summary>
        public object BtnSayClick()
        {
            //先获取当前用户信息
            UsersInfo use = new UsersInfo();
            use.UserAccount = Session["UserAccount"].ToString();
            use = new UsersInfoBLL().Select(u => u.UserAccount == use.UserAccount).Cast<UsersInfo>().FirstOrDefault();

            //进行插入
            CommentInfo cm = new CommentInfo()
            {
                MovieID = Convert.ToInt32(Request["MovieID"]),
                UsersID = use.UsersID,
                UserName = use.UserName,
                UserFace = use.UserFace,
                CommentInfo1 = Request["CommentInfo1"].ToString(),
                CommentTime = Convert.ToDateTime(Request["CommentTime"])
            };

            int i = new CommentInfoBLL().Add(cm);
            if (!(i > 0))
            {
                string str = "\"state\":\"false\"";
                return Json(str);
            }
            else
            {
                int count;
                var Comment = new CommentInfoBLL().SelectPage(1, 5, out count, u => u.MovieID == cm.MovieID, u => u.CommentID, false).Cast<CommentInfo>().ToList();

                var lists = (from ea in Comment
                             select new
                             {
                                 ea.CommentID,
                                 ea.MovieID,
                                 ea.UsersID,
                                 ea.UserName,
                                 ea.UserFace,
                                 ea.CommentInfo1,
                                 ea.CommentTime
                             }).Cast<dynamic>().ToList();

                for (int j = 0; j < lists.Count; j++)
                {
                    if (String.IsNullOrEmpty(lists[j].UserFace))
                    {
                        lists[j].UserFace = "无头像.png";
                    }
                }

                return JsonConvert.SerializeObject(lists);
            }
        }

        /// <summary>
        /// 首页评论
        /// </summary>
        public string FirstCommentInfo()
        {
            if (pageNow == 0) return "";

            //将页码变为1
            pageNow = 1;

            int Count = 0;

            var MovieID = Convert.ToInt32(Request["MovieID"]);

            List<CommentInfo> ls = new CommentInfoBLL().SelectPage(pageNow, pageSize, out Count, u => u.MovieID == MovieID, u => u.CommentID, false).Cast<CommentInfo>().ToList();

            ls = new CommentInfoBLL().Fitle(ls);

            pageCount = (Count % pageSize) > 0 ? (Count / pageSize) + 1 : Count / pageSize;
            pageNow = ls.Count > 0 ? 1 : 0;

            return JsonConvert.SerializeObject(ls);
        }

        /// <summary>
        /// 末页评论
        /// </summary>
        public string EndCommentInfo()
        {
            if (pageNow == 0) return "";
            pageNow = pageCount;

            int Count = 0;

            var MovieID = Convert.ToInt32(Request["MovieID"]);

            List<CommentInfo> ls = new CommentInfoBLL().SelectPage(pageNow, pageSize, out Count, u => u.MovieID == MovieID, u => u.CommentID, false).Cast<CommentInfo>().ToList();

            ls = new CommentInfoBLL().Fitle(ls);

            pageCount = (Count % pageSize) > 0 ? (Count / pageSize) + 1 : Count / pageSize;
            pageNow = ls.Count > 0 ? pageCount : 0;

            var jsondata = JsonConvert.SerializeObject(ls);

            return jsondata;
        }

        /// <summary>
        /// 上一页
        /// </summary>
        public string PrevCommentInfo()
        {
            //如果没值或者已经是第一页，则直接返回
            if (pageNow == 0 || pageNow == 1) return "";

            //不然的话就返回上一页
            pageNow -= 1;

            int Count = 0;

            var MovieID = Convert.ToInt32(Request["MovieID"]);

            List<CommentInfo> ls = new CommentInfoBLL().SelectPage(pageNow, pageSize, out Count, u => u.MovieID == MovieID, u => u.CommentID, false).Cast<CommentInfo>().ToList();

            ls = new CommentInfoBLL().Fitle(ls);

            pageNow = ls.Count > 0 ? pageNow : 0;
            pageCount = (Count % pageSize) > 0 ? (Count / pageSize) + 1 : Count / pageSize;

            var jsondata = JsonConvert.SerializeObject(ls);

            return jsondata;
        }

        /// <summary>
        /// 下一页
        /// </summary>
        public string NextCommentInfo()
        {
            //如果没值或者已经是最后一页，则直接返回
            if (pageNow == 0 || pageNow == pageCount) return "";

            //不然的话就返回上一页
            pageNow += 1;

            int Count = 0;

            var MovieID = Convert.ToInt32(Request["MovieID"]);

            List<CommentInfo> ls = new CommentInfoBLL().SelectPage(pageNow, pageSize, out Count, u => u.MovieID == MovieID, u => u.CommentID, false).Cast<CommentInfo>().ToList();

            ls = new CommentInfoBLL().Fitle(ls);

            pageNow = ls.Count > 0 ? pageNow : 0;
            pageCount = (Count % pageSize) > 0 ? (Count / pageSize) + 1 : Count / pageSize;

            var jsondata = JsonConvert.SerializeObject(ls);

            return jsondata;
        }
    }
}
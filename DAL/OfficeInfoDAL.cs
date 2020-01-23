using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class OfficeInfoDAL : BaseDAL<OfficeInfo>
    {
        /// <summary>
        /// 传入影片ID和影院ID时找排片
        /// </summary>
        /// <param name="mi"></param>
        /// <param name="om"></param>
        /// <returns></returns>
        public List<dynamic> FindShowDetails(MovieInfo mi, CinemaInfo om)
        {
            var linq = (from mv in ef.MovieInfo
                        where mv.MovieID == mi.MovieID
                        select new { mv.MovieName, mv.MovieID }
                        ).Distinct().OrderBy(u => u.MovieID).FirstOrDefault().MovieName;

            var Linq = (from ci in ef.CinemaInfo
                        join oi in ef.OfficeInfo
                        on ci.CinemaID equals oi.CinemaID
                        join cp in ef.ChipInfo
                        on oi.OfficeID equals cp.OfficeID
                        join mt in ef.MovieInfo
                        on cp.MovieName equals mt.MovieName
                        where ci.CinemaID == om.CinemaID && mt.MovieName == linq
                        && DbFunctions.DiffMinutes(DateTime.Now, cp.StartTime) > 15
                        select new
                        {
                            cp.StartTime,
                            cp.StopTime,
                            Language = mt.MovieArea,
                            oi.OfficeID,
                            cp.ChipInfoID,
                            Money = mt.MovieMoney,
                            oi.OfficeName
                        }).ToList<dynamic>();

            return Linq;
        }

        /// <summary>
        /// 只传入影院ID时排片
        /// </summary>
        /// <param name="om"></param>
        /// <returns></returns>
        public List<dynamic> FindShowDetails(CinemaInfo om)
        {
            //先通过影院ID获取第一个电影名
            var linq = (from mv in ef.MovieInfo
                        join cd in ef.ChipInfo
                        on mv.MovieName equals cd.MovieName
                        where cd.CinemaID == om.CinemaID
                        && DbFunctions.DiffMinutes(DateTime.Now, cd.StartTime) > 15
                        select new { mv.MovieName, mv.MovieID }
                        ).Distinct().OrderBy(u => u.MovieID).FirstOrDefault().MovieName;

            //通过电影名查询该电影院Top1的排片
            var Linq = (from ci in ef.CinemaInfo
                        join oi in ef.OfficeInfo
                        on ci.CinemaID equals oi.CinemaID
                        join cp in ef.ChipInfo
                        on oi.OfficeID equals cp.OfficeID
                        join mi in ef.MovieInfo
                        on cp.MovieName equals mi.MovieName
                        where ci.CinemaID == om.CinemaID && mi.MovieName == linq
                        && DbFunctions.DiffMinutes(DateTime.Now, cp.StartTime) > 15
                        select new
                        {
                            cp.StartTime,
                            cp.StopTime,
                            Language = mi.MovieArea,
                            oi.OfficeID,
                            cp.ChipInfoID,
                            Money = mi.MovieMoney,
                            oi.OfficeName
                        }).ToList<dynamic>();

            return Linq;
        }

        /// <summary>
        /// 影院排片的电影信息
        /// </summary>
        /// <param name="mi"></param>
        /// <param name="ci"></param>
        /// <returns></returns>
        public IQueryable FindMovieInfo(MovieInfo mi, CinemaInfo ci)
        {
            var linq = (from mv in ef.MovieInfo
                        join cp in ef.ChipInfo
                        on mv.MovieName equals cp.MovieName
                        where cp.CinemaID == ci.CinemaID
                        && DbFunctions.DiffMinutes(DateTime.Now, cp.StartTime) > 15
                        select mv).Distinct();

            return linq;
        }

        /// <summary>
        /// 该电影今日排片
        /// </summary>
        /// <param name="mv"></param>
        /// <param name="om"></param>
        /// <returns></returns>
        public List<dynamic> FindTod(MovieInfo mv, CinemaInfo om)
        {
            var time1 = Convert.ToDateTime(Convert.ToString(DateTime.Today).Substring(0, 9) + " 00:00:00");
            var time2 = Convert.ToDateTime(Convert.ToString(DateTime.Today).Substring(0, 9) + " 23:59:59");
            var Linq = (from ch in ef.ChipInfo
                        join mi in ef.MovieInfo
                        on ch.MovieName equals mi.MovieName
                        join ofi in ef.OfficeInfo
                        on ch.OfficeID equals ofi.OfficeID
                        where ch.CinemaID == om.CinemaID
                        && ch.MovieName == mv.MovieName
                        && ch.StartTime >= time1
                        && DbFunctions.DiffMinutes(DateTime.Now, ch.StartTime) > 15
                        && ch.StartTime < time2
                        select new
                        {
                            ch.MovieName,
                            ch.StartTime,
                            ch.StopTime,
                            Language = mi.MovieArea,
                            ofi.OfficeID,
                            ofi.OfficeName,
                            ch.ChipInfoID,
                            Money = mi.MovieMoney
                        }).ToList<dynamic>();

            return Linq;
        }

        /// <summary>
        /// 该电影明日排片
        /// </summary>
        /// <param name="mi"></param>
        /// <param name="om"></param>
        /// <returns></returns>
        public List<dynamic> FindTom(MovieInfo mv, CinemaInfo om)
        {
            var time1 = Convert.ToDateTime(Convert.ToString(DateTime.Today).Substring(0, 9) + " 23:59:00");
            var time2 = Convert.ToDateTime(Convert.ToString(DateTime.Today.AddDays(1)).Substring(0, 9) + " 23:59:59");
            var Linq = (from ch in ef.ChipInfo
                        join mi in ef.MovieInfo
                        on ch.MovieName equals mi.MovieName
                        join ofi in ef.OfficeInfo
                        on ch.OfficeID equals ofi.OfficeID
                        where ch.CinemaID == om.CinemaID
                        && ch.MovieName == mv.MovieName
                        && ch.StartTime >= time1
                        && ch.StartTime < time2
                        select new
                        {
                            ch.StartTime,
                            ch.StopTime,
                            Language = mi.MovieArea,
                            ofi.OfficeID,
                            ofi.OfficeName,
                            ch.ChipInfoID,
                            Money = mi.MovieMoney
                        }).ToList<dynamic>();

            return Linq;
        }
    }
}
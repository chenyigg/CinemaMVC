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
                        select mv.MovieName).Distinct().FirstOrDefault();

            var Linq = (from ci in ef.CinemaInfo
                        join oi in ef.OfficeInfo
                        on ci.CinemaID equals oi.CinemaID
                        join cp in ef.ChipInfo
                        on oi.OfficeID equals cp.OfficeID
                        join mt in ef.MovieInfo
                        on cp.MovieName equals mt.MovieName
                        where ci.CinemaID == 1 && mi.MovieName == linq
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
                        select mv.MovieName
                        ).Distinct().FirstOrDefault();

            //通过电影名查询该电影院Top1的排片
            var Linq = (from ci in ef.CinemaInfo
                        join oi in ef.OfficeInfo
                        on ci.CinemaID equals oi.CinemaID
                        join cp in ef.ChipInfo
                        on oi.OfficeID equals cp.OfficeID
                        join mi in ef.MovieInfo
                        on cp.MovieName equals mi.MovieName
                        where ci.CinemaID == 1 && mi.MovieName == linq
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
    }
}
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MovieInfoDAL : BaseDAL<MovieInfo>
    {
        public IQueryable SelectScoreMovieInfo()
        {
            var linq = (from item in ef.MovieInfo
                        orderby item.MovieScore descending
                        select item).Take(10);

            return linq;
        }

        public IQueryable SelectLbMovieInfo()
        {
            var linq = (from item in ef.MovieInfo
                        select item).Take(7);

            return linq;
        }

        /// <summary>
        /// 今日票房
        /// </summary>
        /// <returns></returns>
        public IQueryable SelectMovieMoneyTop()
        {
            //group new{放入要使用聚合函数的列} by new {放入要进行分组的列}
            var linq = (from m in ef.MovieInfo
                        join o in ef.OrderInfo
                        on m.MovieName equals o.MovieName
                        where o.OrderTime > DateTime.Today
                        group new { o.OrderSumMoney } by new { m.MovieID, m.MovieName, m.MovieCover }
                        into g
                        select new
                        {
                            g.Key.MovieID,
                            g.Key.MovieName,
                            g.Key.MovieCover,
                            //此处的OrderSumMoney 表示同一电影的当天票房
                            OrderSumMoney = g.Sum(pp => pp.OrderSumMoney)
                        }).OrderByDescending(g => g.OrderSumMoney).Take(10);

            return linq;
        }
    }
}
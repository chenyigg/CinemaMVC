using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CinemaInfoDAL : BaseDAL<CinemaInfo>
    {
        public List<CinemaInfo> Filtrate(string cinemaArea, string officeName)
        {
            var Linq = (from c in ef.CinemaInfo
                        join o in ef.OfficeInfo
                        on c.CinemaID equals o.CinemaID
                        where c.CinemaArea.Contains(cinemaArea) && o.OfficeName.Contains(officeName)
                        select c).Distinct();

            return Linq.ToList<CinemaInfo>();
        }

        public dynamic ChooseCinemaInfo(MovieInfo model)
        {
            //--查询正在放映此电影的所有电影
            //--开场十五分钟前

            //使用原生Sql语句
            //var Linq = ef.Database.SqlQuery<CinemaInfo>($"select * from CinemaInfo where CinemaID in (select CinemaID from ChipInfo where MovieName = (select MovieName from MovieInfo where MovieID = {model.MovieID}) and datediff(MINUTE, getdate(), StartTime) > 15)");

            var Linq = (from mv in ef.MovieInfo
                        join cp in ef.ChipInfo
                        on mv.MovieName equals cp.MovieName
                        join ci in ef.CinemaInfo
                        on cp.CinemaID equals ci.CinemaID
                        where mv.MovieID == model.MovieID && DbFunctions.DiffMinutes(cp.StartTime, DateTime.Now) > 15
                        select ci).Distinct();

            //下面这句设置成断点就可以看到上一句Linq生成的SQL语句
            //var psql = Linq.GetType().GetMethod("ToTraceString").Invoke(Linq, null);

            //return Linq.ToList();
            return Linq;
        }
    }
}
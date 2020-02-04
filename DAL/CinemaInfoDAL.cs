using Model;
using System;
using System.Collections.Generic;
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
    }
}
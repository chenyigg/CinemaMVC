using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CinemaInfoBLL : BaseBLL<CinemaInfo>
    {
        public List<CinemaInfo> Filtrate(string CinemaArea, string OfficeName)
        {
            if (CinemaArea == "全部")
            {
                CinemaArea = "";
            }
            if (OfficeName == "全部")
            {
                OfficeName = "";
            }
            return new CinemaInfoDAL().Filtrate(CinemaArea, OfficeName);
        }

        public dynamic ChooseCinemaInfo(MovieInfo model)
        {
            return new CinemaInfoDAL().ChooseCinemaInfo(model);
        }
    }
}
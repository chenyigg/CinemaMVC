using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL;

namespace BLL
{
    public class UrlPageInfoBLL : BaseBLL<UrlCountInnfo>
    {
        public int UpdateNow()
        {
            return new UrlPageInfoDAL().UpdateNow();
        }
    }
}
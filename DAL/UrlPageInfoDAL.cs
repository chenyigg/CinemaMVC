using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL;

namespace DAL
{
    public class UrlPageInfoDAL : BaseDAL<UrlCountInnfo>
    {
        internal int UpdateNow()
        {
            return ef.Database.ExecuteSqlCommand("update UrlCountInnfo Set Msg = Msg+1");
        }
    }
}
using Model;
using System.Data.Entity;
using System.Runtime.Remoting.Messaging;

namespace DAL
{
    public class EFFactory
    {
        /// <summary>
        /// 保证EF对象线程内唯一
        /// </summary>
        /// <returns></returns>
        public static DbContext CreateEF()
        {
            DbContext db = CallContext.GetData("DbContext") as DbContext;
            if (db == null)
            {
                db = new CinemaEntities1();

                //进行存储
                CallContext.SetData("DbContext", db);
            }
            //设置为False，防止Json序列化时循环引用
            db.Configuration.ProxyCreationEnabled = false;
            return db;
        }
    }
}
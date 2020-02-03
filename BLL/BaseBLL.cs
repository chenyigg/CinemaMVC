using DAL;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BLL
{
    public class BaseBLL<T> : IBaseBLL<T> where T : class, new()
    {
        private BaseDAL<T> dal = new BaseDAL<T>();

        public bool Add(T entity)
        {
            dal.Add(entity);
            return dal.SaveChanges();
        }

        public bool Delete(T entity)
        {
            dal.Delete(entity);
            return dal.SaveChanges();
        }

        public bool Update(T entity)
        {
            dal.Update(entity);
            return dal.SaveChanges();
        }

        public IQueryable Select(Expression<Func<T, bool>> wherelambda)
        {
            return dal.Select(wherelambda);
        }

        public IQueryable SelectPage<s>(int pageIndex, int pageSize, out int count, Expression<Func<T, bool>> wherelambda, Expression<Func<T, s>> orderbylambda, bool isAsc)
        {
            return dal.SelectPage(pageIndex, pageSize, out count, wherelambda, orderbylambda, isAsc);
        }
    }
}
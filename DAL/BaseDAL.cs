using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Model;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// 具体实现
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseDAL<T> : IBaseDAL<T> where T : class, new()
    {
        //创建EF对象
        public CinemaEntities1 ef = EFFactory.CreateEF() as CinemaEntities1;

        public void Add(T entity)
        {
            ef.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            ef.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            ef.Entry<T>(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public IQueryable Select(Expression<Func<T, bool>> wherelambda)
        {
            return ef.Set<T>().Where(wherelambda);
        }

        public IQueryable SelectPage<s>(int pageIndex, int pageSize, out int count, Expression<Func<T, bool>> wherelambda, Expression<Func<T, s>> orderbylambda, bool isAsc)
        {
            //是否升序
            if (isAsc)
            {
                count = ef.Set<T>().Where(wherelambda).Count();
                return ef.Set<T>().Where(wherelambda).OrderBy(orderbylambda).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
            else
            {
                count = ef.Set<T>().Where(wherelambda).Count();
                return ef.Set<T>().Where(wherelambda).OrderByDescending(orderbylambda).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
        }

        public bool SaveChanges()
        {
            return ef.SaveChanges() > 0;
        }
    }
}
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BLL
{
    public interface IBaseBLL<T> where T : class, new()
    {
        /// <summary>
        /// 增
        /// </summary>
        /// <param name="entity">需要打上标记的类</param>
        /// <returns></returns>
        int Add(T entity);

        /// <summary>
        /// 删
        /// </summary>
        /// <param name="entity">需要打上标记的类</param>
        /// <returns></returns>
        int Delete(T entity);

        /// <summary>
        /// 改
        /// </summary>
        /// <param name="entity">需要打上标记的类</param>
        /// <returns></returns>
        int Update(T entity);

        /// <summary>
        /// 查
        /// </summary>
        /// <param name="wherelambda">查询表达式</param>
        /// <returns></returns>
        IQueryable Select(Expression<Func<T, bool>> wherelambda);

        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页面显示数量</param>
        /// <param name="count">总行数</param>
        /// <param name="wherelambda">查询表达式</param>
        /// <param name="orderbylambda">排序表达式</param>
        /// <param name="isAsc">是否升序</param>
        /// <returns></returns>
        IQueryable SelectPage<s>(int pageIndex, int pageSize, out int count, Expression<Func<T, bool>> wherelambda, Expression<Func<T, s>> orderbylambda, bool isAsc);
    }
}
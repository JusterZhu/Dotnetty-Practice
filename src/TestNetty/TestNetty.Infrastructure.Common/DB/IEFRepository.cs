using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace TestNetty.Infrastructure.Common.DB
{
    public interface IEFRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <param name="id">实体主键ID</param>
        /// <returns></returns>
        TEntity Find(object id);
        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <param name="condition">筛选条件</param>
        /// <returns></returns>
        TEntity Find(Expression<Func<TEntity, bool>> condition);
        /// <summary>
        /// 判断实体是否存在
        /// </summary>
        /// <param name="condition">筛选条件</param>
        /// <returns></returns>
        bool Exist(Expression<Func<TEntity, bool>> condition);
        /// <summary>
        /// 获取全部实体列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> GetAllList();
        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <param name="condition">筛选条件</param>
        /// <returns></returns>
        IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> condition);
        /// <summary>
        /// 获取实体列表数量
        /// </summary>
        /// <param name="condition">筛选条件</param>
        /// <returns></returns>
        long GetCount(Expression<Func<TEntity, bool>> condition);
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        bool Add(TEntity entity);
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        bool Delete(TEntity entity);
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="condition">筛选条件</param>
        /// <returns></returns>
        bool Delete(Expression<Func<TEntity, bool>> condition);
        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        bool Update(TEntity entity);
        /// <summary>
        /// 查找分页数据列表
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="condition">查询表达式</param>
        /// <param name="orderBy">排序表达式</param>
        /// <param name="IsAsc">是否升序,默认为升序</param>
        /// <returns>实体集合</returns>
        IList<TEntity> GetPagedList(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> condition, Func<TEntity, object> orderBy, bool IsAsc = true);
    }
}

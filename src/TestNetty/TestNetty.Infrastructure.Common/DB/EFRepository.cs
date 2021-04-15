using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace TestNetty.Infrastructure.Common.DB
{
    public class EFRepository<TEntity> : IEFRepository<TEntity> where TEntity : class
    {
        /// <summary>
		/// 数据库上下文
		/// </summary>
		public DbContext Context { get; set; }

        public EFRepository(DbContext context)
        {
            Context = context;
        }

        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <param name="id">实体主键ID</param>
        /// <returns></returns>
        public virtual TEntity Find(object id)
        {
            try
            {
                return Context.Set<TEntity>().Find(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <param name="condition">筛选条件</param>
        /// <returns></returns>
        public virtual TEntity Find(Expression<Func<TEntity, bool>> condition)
        {
            try
            {
                return Context.Set<TEntity>().FirstOrDefault(condition);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        /// <summary>
        /// 判断实体是否存在
        /// </summary>
        /// <param name="condition">筛选条件</param>
        /// <returns></returns>
        public virtual bool Exist(Expression<Func<TEntity, bool>> condition)
        {
            try
            {
                return Context.Set<TEntity>().Any(condition);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        /// <summary>
        /// 获取全部实体列表
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetAllList()
        {
            try
            {
                return Context.Set<TEntity>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <param name="condition">筛选条件</param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> condition)
        {
            try
            {
                return Context.Set<TEntity>().Where(condition);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 获取实体列表数量
        /// </summary>
        /// <param name="condition">筛选条件</param>
        /// <returns></returns>
        public virtual long GetCount(Expression<Func<TEntity, bool>> condition)
        {
            try
            {
                return Context.Set<TEntity>().Count(condition);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public virtual bool Add(TEntity entity)
        {
            try
            {
                Context.Entry<TEntity>(entity).State = EntityState.Added;
                var result = Context.SaveChanges();
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// TODO:添加或更新实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public virtual bool AddOrUpdate(TEntity entity)
        {
            try
            {
                Context.Set<TEntity>();//.AddOrUpdate(entity);
                var result = Context.SaveChanges();
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public virtual bool Delete(TEntity entity)
        {
            try
            {
                Context.Entry<TEntity>(entity).State = EntityState.Deleted;
                return Context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public virtual bool Delete(Expression<Func<TEntity, bool>> condition)
        {
            try
            {
                var entity = Context.Set<TEntity>().Find(condition);
                Context.Entry<TEntity>(entity).State = EntityState.Deleted;
                return Context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        /// <summary>
        /// 删除多个实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public virtual bool DeleteList(Expression<Func<TEntity, bool>> condition)
        {
            try
            {
                var list = Context.Set<TEntity>().Where(condition);
                foreach (var item in list)
                {
                    Context.Entry<TEntity>(item).State = EntityState.Deleted;
                }
                return Context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public virtual bool Update(TEntity entity)
        {
            try
            {
                Context.Entry<TEntity>(entity).State = EntityState.Modified;
                return Context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public IList<TEntity> GetPagedList(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> condition, Func<TEntity, object> orderBy, bool IsAsc = true)
        {
            throw new NotImplementedException();
        }

    }
}

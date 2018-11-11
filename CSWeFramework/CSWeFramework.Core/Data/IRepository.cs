using System.Collections.Generic;
using System.Linq;

namespace CSWeFramework.Core.Data
{
    /// <summary>
    /// 仓储抽象接口。通用公共方法
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Get Entity By Identity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById(object id);

        /// <summary>
        /// Insert entity
        /// </summary>
        /// <param name="entity"></param>
        void Insert(T entity);

        /// <summary>
        /// Insert entitys
        /// </summary>
        /// <param name="entitys"></param>
        void Insert(IEnumerable<T> entitys);

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);

        /// <summary>
        /// Update entitys
        /// </summary>
        /// <param name="entitys"></param>
        void Update(IEnumerable<T> entitys);

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);

        /// <summary>
        /// Delete entitys
        /// </summary>
        /// <param name="entitys"></param>
        void Delete(IEnumerable<T> entitys);

        IQueryable<T> Table { get; }

    }
}

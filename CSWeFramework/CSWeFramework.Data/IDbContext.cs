using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSWeFramework.Data
{
    /// <summary>
    /// 数据库上下文接口
    /// </summary>
    public interface IDbContext
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;

        int SaveChanges();

        int ExcuteSqlCommand(string sql, params object[] parameters);
    }
}

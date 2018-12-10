using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Reflection;

namespace CSWeFramework.Data
{
    public class CarDbContext : DbContext, IDbContext
    {
        static CarDbContext()
        {
            //数据库初始化,不存在时重新创建数据库实例
            Database.SetInitializer(new CreateDatabaseIfNotExists<CarDbContext>());
        }

        public CarDbContext() : base("carDatabase")
        {

        }

        public int ExcuteSqlCommand(string sql, params object[] parameters)
        {
            return this.Database.ExecuteSqlCommand(sql, parameters);
        }

        IDbSet<TEntity> IDbContext.Set<TEntity>()
        {
            return base.Set<TEntity>();
        }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //ef创建表，自带复数功能删除掉
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //将程序及当中实现了EntityTypeConfiguration的类,配置的实体信息加载进来，
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

      

       
    }
}

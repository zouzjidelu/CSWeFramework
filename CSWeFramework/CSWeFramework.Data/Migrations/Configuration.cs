namespace CSWeFramework.Data.Migrations
{
    using CSWeFramework.Core.Domain;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CSWeFramework.Data.CarDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(CSWeFramework.Data.CarDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            //��ĿǨ�ƣ���ʼ������
            context.Set<Car>().AddOrUpdate(
                new Car { Name = "����", Price = 38, CreateTime = DateTime.Now },
                new Car { Name = "�µ�", Price = 59, CreateTime = DateTime.Now },
                new Car { Name = "������", Price = 98, CreateTime = DateTime.Now }
                );
        }
    }
}

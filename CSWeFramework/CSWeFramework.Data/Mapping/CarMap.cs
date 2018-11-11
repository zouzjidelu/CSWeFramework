using CSWeFramework.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSWeFramework.Data.Mapping
{
    public class CarMap : EntityTypeConfiguration<Car>
    {
        public CarMap()
        {
            this.ToTable("Car");
            this.HasKey(c => c.ID);
            this.Property(c => c.Name).HasMaxLength(20).IsRequired();
        }
    }
}

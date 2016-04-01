using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FARMC.Models;

namespace FARMC.DAL
{
    public class FishCatchConfiguration : EntityTypeConfiguration<FishCatch>
    {
        public FishCatchConfiguration()
        {
            Property(fc => fc.FishName).HasMaxLength(20).IsRequired();
            Property(fc => fc.Qty).IsRequired();
            Property(fc => fc.UnitPrice).IsRequired();
            Ignore(fc => fc.ObjectState);
        }
    }
}

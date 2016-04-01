using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FARMC.Models;

namespace FARMC.DAL
{
    public class FisherfolkConfiguration : EntityTypeConfiguration<Fisherfolk>
    {
        public FisherfolkConfiguration()
        {
            Property(ff => ff.LastName).HasMaxLength(30).IsRequired();
            Property(ff => ff.FisherId).HasMaxLength(15).IsRequired();
            Ignore(ff => ff.ObjectState);
        }
    }
}

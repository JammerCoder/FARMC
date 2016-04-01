using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FARMC.Models;

namespace FARMC.DAL
{
    public class FARMCDbContext : DbContext
    {
        public FARMCDbContext() : base("FARMC")
        {
        }

        public DbSet<Fisherfolk> Fisherfolks { get; set; }
        public DbSet<FishCatch> FishCatches { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new FisherfolkConfiguration());
            modelBuilder.Configurations.Add(new FishCatchConfiguration());
        }
    }
}

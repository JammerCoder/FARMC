using FARMC.Models;

namespace FARMC.DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FARMC.DAL.FARMCDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(FARMC.DAL.FARMCDbContext context)
        {
            context.Fisherfolks.AddOrUpdate(
                ff => ff.LastName,
                new Fisherfolk
                {
                    LastName = "Barro",
                    FisherId = "L001",
                    FishCatches =
                    {
                       new FishCatch{FishName = "Bangsi", Qty = 30.0m, UnitPrice = 55.0m},
                       new FishCatch{FishName = "Matambaka", Qty = 15.0m, UnitPrice = 85.0m},
                       new FishCatch{FishName = "Nukos", Qty = 18.0m, UnitPrice = 65.0m}
                    }
                        
                },
                new Fisherfolk { LastName = "Muñoz", FisherId = "L002" },
                new Fisherfolk { LastName = "Pernites", FisherId = "L003" }
                );

        }
    }
}

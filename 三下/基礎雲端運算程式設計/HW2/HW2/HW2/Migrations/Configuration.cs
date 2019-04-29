namespace HW2.Migrations
{
    using HW2.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<HW2.Models.Model1>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(HW2.Models.Model1 context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.TestTables.AddOrUpdate(
              p => new { p.name, p.comment, p.date },
                  new TestTB { name = "AAA", comment = "你好", date = DateTime.Parse("2019/4/28").Date },
                  new TestTB { name = "BBB", comment = "午安", date = DateTime.Parse("2019/4/28").Date },
                  new TestTB { name = "CCC", comment = "晚安", date = DateTime.Parse("2019/4/28").Date }
                );
        }
    }
}

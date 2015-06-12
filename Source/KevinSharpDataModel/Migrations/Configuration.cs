namespace KevinSharp.DataModel.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<KevinSharp.DataModel.KevinSharpDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(KevinSharp.DataModel.KevinSharpDbContext context)
        {
            Seeder.Seed(context);
        }
    }
}

namespace Trip.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Trip.Models.TripContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Trip.Models.TripContext context)
        {
            Trip.Models.Seed.DBSeed(context);
        }
    }
}

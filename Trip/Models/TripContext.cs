using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Trip.Models
{
    public class TripContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public TripContext() : base("name=TripContext")
        {
        }

        public System.Data.Entity.DbSet<Trip.Models.Region> Regions { get; set; }
        public System.Data.Entity.DbSet<Trip.Models.Destination> Destinations { get; set; }
        public System.Data.Entity.DbSet<Trip.Models.Activity> Activities { get; set; }
        public System.Data.Entity.DbSet<Trip.Models.Sequence> Sequences { get; set; }
        public System.Data.Entity.DbSet<Trip.Models.SequenceToActivity> SequencesToActivities { get; set; }
        public System.Data.Entity.DbSet<Trip.Models.Vacation> Vacations { get; set; }
        public System.Data.Entity.DbSet<Trip.Models.VacationToSequence> VacationsToSequences { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SequenceToActivity>().HasRequired(a => a.Activity).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<VacationToSequence>().HasRequired(s => s.Sequence).WithMany().WillCascadeOnDelete(false);
            base.OnModelCreating(modelBuilder);
        }
    }
}

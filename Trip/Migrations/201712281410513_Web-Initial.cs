namespace Trip.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WebInitial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        ActivityID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Type = c.Int(nullable: false),
                        Distance = c.Double(nullable: false),
                        Duration = c.Double(nullable: false),
                        ExperienceRating = c.Int(nullable: false),
                        TechnicalRating = c.Int(nullable: false),
                        EnduranceRating = c.Int(nullable: false),
                        ElusiveRating = c.Int(nullable: false),
                        Dest1ID = c.Int(nullable: false),
                        Dest2ID = c.Int(nullable: false),
                        Reversible = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ActivityID);
            
            CreateTable(
                "dbo.Destinations",
                c => new
                    {
                        DestinationID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        RegionID = c.Int(nullable: false),
                        NatureRating = c.Int(nullable: false),
                        NightlifeRating = c.Int(nullable: false),
                        ShoppingRating = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DestinationID);
            
            CreateTable(
                "dbo.Regions",
                c => new
                    {
                        RegionID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.RegionID);
            
            CreateTable(
                "dbo.Sequences",
                c => new
                    {
                        SequenceID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        RegionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SequenceID);
            
            CreateTable(
                "dbo.SequenceToActivities",
                c => new
                    {
                        SequenceToActivityID = c.Int(nullable: false, identity: true),
                        SequenceID = c.Int(nullable: false),
                        ActivityID = c.Int(nullable: false),
                        Seqnr = c.Int(nullable: false),
                        Reverted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.SequenceToActivityID)
                .ForeignKey("dbo.Activities", t => t.ActivityID)
                .ForeignKey("dbo.Sequences", t => t.SequenceID, cascadeDelete: true)
                .Index(t => t.SequenceID)
                .Index(t => t.ActivityID);
            
            CreateTable(
                "dbo.Vacations",
                c => new
                    {
                        VacationID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.VacationID);
            
            CreateTable(
                "dbo.VacationToSequences",
                c => new
                    {
                        VacationToSequenceID = c.Int(nullable: false, identity: true),
                        VacationID = c.Int(nullable: false),
                        SequenceID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VacationToSequenceID)
                .ForeignKey("dbo.Sequences", t => t.SequenceID)
                .ForeignKey("dbo.Vacations", t => t.VacationID, cascadeDelete: true)
                .Index(t => t.VacationID)
                .Index(t => t.SequenceID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VacationToSequences", "VacationID", "dbo.Vacations");
            DropForeignKey("dbo.VacationToSequences", "SequenceID", "dbo.Sequences");
            DropForeignKey("dbo.SequenceToActivities", "SequenceID", "dbo.Sequences");
            DropForeignKey("dbo.SequenceToActivities", "ActivityID", "dbo.Activities");
            DropIndex("dbo.VacationToSequences", new[] { "SequenceID" });
            DropIndex("dbo.VacationToSequences", new[] { "VacationID" });
            DropIndex("dbo.SequenceToActivities", new[] { "ActivityID" });
            DropIndex("dbo.SequenceToActivities", new[] { "SequenceID" });
            DropTable("dbo.VacationToSequences");
            DropTable("dbo.Vacations");
            DropTable("dbo.SequenceToActivities");
            DropTable("dbo.Sequences");
            DropTable("dbo.Regions");
            DropTable("dbo.Destinations");
            DropTable("dbo.Activities");
        }
    }
}

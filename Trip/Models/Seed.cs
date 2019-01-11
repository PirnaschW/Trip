using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Trip.Models
{
    public class Seed
    {
        public static void DBSeed(Trip.Models.TripContext context)
        {
            List<Region> r = new List<Region>();
            r.Add(new Region { RegionID = 1, Name = "Moab" });
            r.Add(new Region { RegionID = 2, Name = "Escalante" });
            context.Regions.AddOrUpdate(R => R.RegionID, r.ToArray());

            List<Destination> d = new List<Destination>();
            d.Add(new Destination { DestinationID = 1, Name = "Double-O-Arch", RegionID = 1, NatureRating = 4, NightlifeRating = 1, ShoppingRating = 1 });
            d.Add(new Destination { DestinationID = 2, Name = "Grandview Point", RegionID = 1, NatureRating = 5, NightlifeRating = 1, ShoppingRating = 1 });
            d.Add(new Destination { DestinationID = 3, Name = "Delicate Arch", RegionID = 1, NatureRating = 4, NightlifeRating = 1, ShoppingRating = 1 });
            d.Add(new Destination { DestinationID = 4, Name = "Moab Downtown", RegionID = 1, NatureRating = 1, NightlifeRating = 2, ShoppingRating = 4 });
            d.Add(new Destination { DestinationID = 5, Name = "Zebra Canyon", RegionID = 2, NatureRating = 5, NightlifeRating = 1, ShoppingRating = 1 });
            d.Add(new Destination { DestinationID = 6, Name = "Golden Cathedral", RegionID = 2, NatureRating = 4, NightlifeRating = 1, ShoppingRating = 1 });
            d.Add(new Destination { DestinationID = 7, Name = "Neon Canyon", RegionID = 2, NatureRating = 3, NightlifeRating = 1, ShoppingRating = 1 });
            d.Add(new Destination { DestinationID = 8, Name = "Arches National Park", RegionID = 1, NatureRating = 3, NightlifeRating = 1, ShoppingRating = 2 });
            context.Destinations.AddOrUpdate(D => D.DestinationID, d.ToArray());

            List<Activity> a = new List<Activity>();
            a.Add(new Activity { ActivityID = 1, Name = "Devil's Garden", Type = Trip.Models.ActivityType.Hike, Distance = 6, Duration = 180, ExperienceRating = 5, TechnicalRating = 3, EnduranceRating = 3, ElusiveRating = 2, Reversible = true, Start = d[0], End = d[7] });
            a.Add(new Activity { ActivityID = 2, Name = "Grandview Point", Type = Trip.Models.ActivityType.Look, Distance = 0.1, Duration = 5, ExperienceRating = 5, TechnicalRating = 1, EnduranceRating = 1, ElusiveRating = 2, Reversible = true, Start = d[1], End = d[1] });
            a.Add(new Activity { ActivityID = 3, Name = "Shop til you Drop", Type = Trip.Models.ActivityType.Look, Distance = 0.1, Duration = 60, ExperienceRating = 1, TechnicalRating = 1, EnduranceRating = 1, ElusiveRating = 1, Reversible = true, Start = d[3], End = d[3] });
            a.Add(new Activity { ActivityID = 4, Name = "Zebra Canyon", Type = Trip.Models.ActivityType.Hike, Distance = 5, Duration = 180, ExperienceRating = 3, TechnicalRating = 2, EnduranceRating = 3, ElusiveRating = 4, Reversible = true, Start = d[4], End = d[4] });
            a.Add(new Activity { ActivityID = 5, Name = "Walk to Neon Canyon", Type = Trip.Models.ActivityType.Hike, Distance = 5, Duration = 120, ExperienceRating = 4, TechnicalRating = 3, EnduranceRating = 3, ElusiveRating = 4, Reversible = true, Start = d[6], End = d[6] });
            a.Add(new Activity { ActivityID = 6, Name = "Neon Canyon to Golden Cathedral", Type = Trip.Models.ActivityType.Hike, Distance = 0.5, Duration = 30, ExperienceRating = 2, TechnicalRating = 3, EnduranceRating = 2, ElusiveRating = 4, Reversible = true, Start = d[5], End = d[6] });
            a.Add(new Activity { ActivityID = 7, Name = "Walk to Delicate Arch", Type = Trip.Models.ActivityType.Hike, Distance = 3, Duration = 150, ExperienceRating = 2, TechnicalRating = 2, EnduranceRating = 3, ElusiveRating = 1, Reversible = true, Start = d[2], End = d[7] });
            context.Activities.AddOrUpdate(A => A.ActivityID, a.ToArray());

            List<Sequence> s = new List<Sequence>();
            s.Add(new Sequence { SequenceID = 1, Name = "Moab Area Highlights" });
            s.Add(new Sequence { SequenceID = 2, Name = "Do It All - Moab" });
            s.Add(new Sequence { SequenceID = 3, Name = "Escalante - Do It All" });
            context.Sequences.AddOrUpdate(S => S.SequenceID, s.ToArray());

            List<SequenceToActivity> sa = new List<SequenceToActivity>();
            sa.Add(new SequenceToActivity { SequenceToActivityID = 1, SequenceID = 1, ActivityID = 1 });
            sa.Add(new SequenceToActivity { SequenceToActivityID = 2, SequenceID = 1, ActivityID = 2 });
            sa.Add(new SequenceToActivity { SequenceToActivityID = 3, SequenceID = 1, ActivityID = 7 });
            sa.Add(new SequenceToActivity { SequenceToActivityID = 4, SequenceID = 3, ActivityID = 4 });
            sa.Add(new SequenceToActivity { SequenceToActivityID = 5, SequenceID = 3, ActivityID = 5 });
            sa.Add(new SequenceToActivity { SequenceToActivityID = 6, SequenceID = 3, ActivityID = 6 });
            sa.Add(new SequenceToActivity { SequenceToActivityID = 7, SequenceID = 2, ActivityID = 1 });
            sa.Add(new SequenceToActivity { SequenceToActivityID = 8, SequenceID = 2, ActivityID = 2 });
            sa.Add(new SequenceToActivity { SequenceToActivityID = 9, SequenceID = 2, ActivityID = 3 });
            sa.Add(new SequenceToActivity { SequenceToActivityID = 10, SequenceID = 2, ActivityID = 7 });
            context.SequencesToActivities.AddOrUpdate(S => S.SequenceToActivityID, sa.ToArray());

            List<Vacation> v = new List<Vacation>();
            v.Add(new Vacation { VacationID = 1, Name = "Two-Week Trip" });
            v.Add(new Vacation { VacationID = 2, Name = "Moab Only" });
            v.Add(new Vacation { VacationID = 3, Name = "Escalante, UT" });
            context.Vacations.AddOrUpdate(V => V.VacationID, v.ToArray());

            List<VacationToSequence> vs = new List<VacationToSequence>();
            vs.Add(new VacationToSequence { VacationToSequenceID = 1, VacationID = 1, SequenceID = 2 });
            vs.Add(new VacationToSequence { VacationToSequenceID = 2, VacationID = 1, SequenceID = 3 });
            vs.Add(new VacationToSequence { VacationToSequenceID = 3, VacationID = 2, SequenceID = 1 });
            vs.Add(new VacationToSequence { VacationToSequenceID = 4, VacationID = 3, SequenceID = 3 });
            context.VacationsToSequences.AddOrUpdate(V => V.VacationToSequenceID, vs.ToArray());

        }
    }
}
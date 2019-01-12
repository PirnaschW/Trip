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
      r.Add(new Region { RegionID = 3, Name = "Las Vegas" });
      context.Regions.AddOrUpdate(R => R.RegionID, r.ToArray());

      List<Destination> d = new List<Destination>();
      d.Add(new Destination { DestinationID = 1, Name = "Double-O-Arch", RegionID = 1, NatureRating = 4, NightlifeRating = 1, ShoppingRating = 1 });
      d.Add(new Destination { DestinationID = 2, Name = "Grandview Point", RegionID = 1, NatureRating = 5, NightlifeRating = 1, ShoppingRating = 1 });
      d.Add(new Destination { DestinationID = 3, Name = "Delicate Arch", RegionID = 1, NatureRating = 4, NightlifeRating = 1, ShoppingRating = 1 });
      d.Add(new Destination { DestinationID = 4, Name = "Moab Downtown", RegionID = 1, NatureRating = 1, NightlifeRating = 2, ShoppingRating = 4 });
      d.Add(new Destination { DestinationID = 5, Name = "Arches National Park", RegionID = 1, NatureRating = 3, NightlifeRating = 1, ShoppingRating = 2 });
      d.Add(new Destination { DestinationID = 6, Name = "Zebra Canyon", RegionID = 2, NatureRating = 5, NightlifeRating = 1, ShoppingRating = 1 });
      d.Add(new Destination { DestinationID = 7, Name = "Golden Cathedral", RegionID = 2, NatureRating = 4, NightlifeRating = 1, ShoppingRating = 1 });
      d.Add(new Destination { DestinationID = 8, Name = "Neon Canyon", RegionID = 2, NatureRating = 3, NightlifeRating = 1, ShoppingRating = 1 });
      d.Add(new Destination { DestinationID = 9, Name = "Escalante, UT", RegionID = 2, NatureRating = 1, NightlifeRating = 2, ShoppingRating = 2 });
      d.Add(new Destination { DestinationID = 10, Name = "Hole-in-the-Rock Road", RegionID = 2, NatureRating = 2, NightlifeRating = 1, ShoppingRating = 1 });
      d.Add(new Destination { DestinationID = 11, Name = "Tunnel Slot", RegionID = 2, NatureRating = 3, NightlifeRating = 1, ShoppingRating = 1 });
      context.Destinations.AddOrUpdate(D => D.DestinationID, d.ToArray());

      List<Activity> a = new List<Activity>();
      a.Add(new Activity { ActivityID = 1, Name = "Grandview Point", Type = Trip.Models.ActivityType.Look, Distance = 0.1, Duration = 5, ExperienceRating = 5, TechnicalRating = 1, EnduranceRating = 1, ElusiveRating = 2, Reversible = true, Dest1ID = 2, Dest2ID = 2 });
      a.Add(new Activity { ActivityID = 2, Name = "Shop til you Drop", Type = Trip.Models.ActivityType.Look, Distance = 0.1, Duration = 60, ExperienceRating = 1, TechnicalRating = 1, EnduranceRating = 1, ElusiveRating = 1, Reversible = true, Dest1ID = 4, Dest2ID = 4 });
      a.Add(new Activity { ActivityID = 3, Name = "Devil's Garden", Type = Trip.Models.ActivityType.Hike, Distance = 6, Duration = 180, ExperienceRating = 5, TechnicalRating = 3, EnduranceRating = 3, ElusiveRating = 2, Reversible = true, Dest1ID = 5, Dest2ID = 1 });
      a.Add(new Activity { ActivityID = 4, Name = "Walk to Delicate Arch", Type = Trip.Models.ActivityType.Hike, Distance = 3, Duration = 150, ExperienceRating = 2, TechnicalRating = 2, EnduranceRating = 3, ElusiveRating = 1, Reversible = true, Dest1ID = 5, Dest2ID = 3 });
      a.Add(new Activity { ActivityID = 5, Name = "Zebra Canyon", Type = Trip.Models.ActivityType.Hike, Distance = 5, Duration = 180, ExperienceRating = 3, TechnicalRating = 2, EnduranceRating = 3, ElusiveRating = 4, Reversible = true, Dest1ID = 6, Dest2ID = 10 });
      a.Add(new Activity { ActivityID = 6, Name = "Walk to Neon Canyon", Type = Trip.Models.ActivityType.Hike, Distance = 5, Duration = 120, ExperienceRating = 4, TechnicalRating = 3, EnduranceRating = 3, ElusiveRating = 4, Reversible = true, Dest1ID = 8, Dest2ID = 10 });
      a.Add(new Activity { ActivityID = 7, Name = "Neon Canyon to Golden Cathedral", Type = Trip.Models.ActivityType.Hike, Distance = 0.5, Duration = 30, ExperienceRating = 2, TechnicalRating = 3, EnduranceRating = 2, ElusiveRating = 4, Reversible = true, Dest1ID = 7, Dest2ID = 8 });
      a.Add(new Activity { ActivityID = 8, Name = "Devil's Garden Trail - Primitive Loop", Type = Trip.Models.ActivityType.Hike, Distance = 2, Duration = 60, ExperienceRating = 2, TechnicalRating = 3, EnduranceRating = 2, ElusiveRating = 2, Reversible = true, Dest1ID = 5, Dest2ID = 1 });
      a.Add(new Activity { ActivityID = 9, Name = "Drive Hole-in-the-Rock Road", Type = Trip.Models.ActivityType.Drive, Distance = 20, Duration = 30, ExperienceRating = 2, TechnicalRating = 2, EnduranceRating = 1, ElusiveRating = 1, Reversible = true, Dest1ID = 10, Dest2ID = 9 });
      a.Add(new Activity { ActivityID = 10, Name = "Zebra-Tunnel Connection", Type = Trip.Models.ActivityType.Hike, Distance = 2, Duration = 30, ExperienceRating = 3, TechnicalRating = 2, EnduranceRating = 2, ElusiveRating = 2, Reversible = true, Dest1ID = 11, Dest2ID = 6 });
      a.Add(new Activity { ActivityID = 11, Name = "Tunnel Slot Hike", Type = Trip.Models.ActivityType.Hike, Distance = 2.5, Duration = 45, ExperienceRating = 3, TechnicalRating = 2, EnduranceRating = 2, ElusiveRating = 2, Reversible = true, Dest1ID = 11, Dest2ID = 10 });
      context.Activities.AddOrUpdate(A => A.ActivityID, a.ToArray());

      List<Sequence> s = new List<Sequence>();
      s.Add(new Sequence { SequenceID = 1, Name = "Moab Area Highlights", RegionID = 1 });
      s.Add(new Sequence { SequenceID = 2, Name = "Do It All - Moab", RegionID = 1 });
      s.Add(new Sequence { SequenceID = 3, Name = "Escalante - Do It All", RegionID = 2 });
      s.Add(new Sequence { SequenceID = 4, Name = "Zebra Canyon and Tunnel Slot", RegionID = 2 });
      context.Sequences.AddOrUpdate(S => S.SequenceID, s.ToArray());

      List<SequenceToActivity> sa = new List<SequenceToActivity>();
      sa.Add(new SequenceToActivity { SequenceToActivityID = 1, SequenceID = 1, ActivityID = 1, Seqnr = 1, Reverted = false });
      sa.Add(new SequenceToActivity { SequenceToActivityID = 2, SequenceID = 1, ActivityID = 3, Seqnr = 2, Reverted = false });
      sa.Add(new SequenceToActivity { SequenceToActivityID = 3, SequenceID = 1, ActivityID = 4, Seqnr = 3, Reverted = false });
      sa.Add(new SequenceToActivity { SequenceToActivityID = 4, SequenceID = 1, ActivityID = 8, Seqnr = 4, Reverted = false });
      sa.Add(new SequenceToActivity { SequenceToActivityID = 5, SequenceID = 2, ActivityID = 3, Seqnr = 1, Reverted = false });
      sa.Add(new SequenceToActivity { SequenceToActivityID = 6, SequenceID = 2, ActivityID = 8, Seqnr = 2, Reverted = false });
      sa.Add(new SequenceToActivity { SequenceToActivityID = 7, SequenceID = 2, ActivityID = 4, Seqnr = 3, Reverted = false });
      sa.Add(new SequenceToActivity { SequenceToActivityID = 8, SequenceID = 2, ActivityID = 1, Seqnr = 4, Reverted = false });
      sa.Add(new SequenceToActivity { SequenceToActivityID = 9, SequenceID = 2, ActivityID = 2, Seqnr = 5, Reverted = false });
      sa.Add(new SequenceToActivity { SequenceToActivityID = 10, SequenceID = 3, ActivityID = 9, Seqnr = 1, Reverted = false });
      sa.Add(new SequenceToActivity { SequenceToActivityID = 11, SequenceID = 3, ActivityID = 5, Seqnr = 2, Reverted = false });
      sa.Add(new SequenceToActivity { SequenceToActivityID = 12, SequenceID = 3, ActivityID = 10, Seqnr = 3, Reverted = false });
      sa.Add(new SequenceToActivity { SequenceToActivityID = 13, SequenceID = 3, ActivityID = 11, Seqnr = 4, Reverted = false });
      sa.Add(new SequenceToActivity { SequenceToActivityID = 14, SequenceID = 3, ActivityID = 6, Seqnr = 5, Reverted = false });
      sa.Add(new SequenceToActivity { SequenceToActivityID = 15, SequenceID = 3, ActivityID = 7, Seqnr = 6, Reverted = false });
      sa.Add(new SequenceToActivity { SequenceToActivityID = 16, SequenceID = 3, ActivityID = 7, Seqnr = 7, Reverted = false });
      sa.Add(new SequenceToActivity { SequenceToActivityID = 17, SequenceID = 3, ActivityID = 6, Seqnr = 8, Reverted = false });
      sa.Add(new SequenceToActivity { SequenceToActivityID = 18, SequenceID = 3, ActivityID = 9, Seqnr = 9, Reverted = false });
      sa.Add(new SequenceToActivity { SequenceToActivityID = 19, SequenceID = 4, ActivityID = 9, Seqnr = 1, Reverted = true });
      sa.Add(new SequenceToActivity { SequenceToActivityID = 20, SequenceID = 4, ActivityID = 5, Seqnr = 2, Reverted = true });
      sa.Add(new SequenceToActivity { SequenceToActivityID = 21, SequenceID = 4, ActivityID = 10, Seqnr = 3, Reverted = true });
      sa.Add(new SequenceToActivity { SequenceToActivityID = 22, SequenceID = 4, ActivityID = 11, Seqnr = 4, Reverted = false });
      sa.Add(new SequenceToActivity { SequenceToActivityID = 23, SequenceID = 4, ActivityID = 9, Seqnr = 5, Reverted = false });
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
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Trip.Models
{
  public enum ActivityType { Drive, Hike, Walk, Look, Show };
  public class ActivityData
  {
    public void CopyData(ActivityData a)  // helper function
    {
      this.ActivityID = a.ActivityID;
      this.Name = a.Name;
      this.Type = a.Type;
      this.Distance = a.Distance;
      this.Duration = a.Duration;
      this.ExperienceRating = a.ExperienceRating;
      this.TechnicalRating = a.TechnicalRating;
      this.EnduranceRating = a.EnduranceRating;
      this.ElusiveRating = a.ElusiveRating;
      this.Dest1ID = a.Dest1ID;
      this.Dest2ID = a.Dest2ID;
      this.Reversible = a.Reversible;
    }

    [Key]
    public int ActivityID { get; set; }

    [Required]
    [MaxLength(50)]
    [Display(Name = "Activity")]
    public string Name { get; set; }

    public ActivityType Type { get; set; }     // Type of the activity

    [Range(0, 300)]
    public double Distance { get; set; }       // Distance in miles

    [Range(0, 24)]
    public double Duration { get; set; }       // Duration in hours

    [Range(0, 5)]
    [Display(Name = "Worthiness")]
    public int ExperienceRating { get; set; }  // how great is it

    [Range(0, 5)]
    [Display(Name = "Difficult")]
    public int TechnicalRating { get; set; }   // how difficult is it technically

    [Range(0, 5)]
    [Display(Name = "Strenous")]
    public int EnduranceRating { get; set; }   // how strenuous is it

    [Range(0, 5)]
    [Display(Name = "Hard to Find")]
    public int ElusiveRating { get; set; }     // how hard is it to find

    [Display(Name = "Start at")]
    public int Dest1ID { get; set; }

    [Display(Name = "End at")]
    public int Dest2ID { get; set; }

    public bool Reversible { get; set; }       // can it been done reversed?
  }

  public class Activity : ActivityData
  {
    public double GetDuration()
    {
      return Duration;
    }
  }

  public class ActivityDisp : ActivityData
  {
    public virtual Destination Dest1 { get; set; }
    public virtual Destination Dest2 { get; set; }

    //    public List<SelectDisp> StartDestinations { get; set; }
    //    public List<SelectDisp> EndDestinations { get; set; }
  }
}
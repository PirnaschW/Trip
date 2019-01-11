using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Trip.Models
{
    public enum ActivityType { Drive, Hike, Walk, Look, Show };
    public class ActivityData
    {
        public void CopyData(ActivityData a)  // helper function
        {
            this.Name             = a.Name;
            this.Type             = a.Type;
            this.Distance         = a.Distance;
            this.Duration         = a.Duration;
            this.ExperienceRating = a.ExperienceRating;
            this.TechnicalRating  = a.TechnicalRating;
            this.EnduranceRating  = a.EnduranceRating;
            this.Start            = a.Start;
            this.End              = a.End;
        }

        [Key]
        public int ActivityID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public ActivityType Type { get; set; }     // Type of the activity

        [Range(0, 300)]
        public double Distance { get; set; }       // Distance in miles

        [Range(0, 480)]
        public double Duration { get; set; }       // Duration in minutes

        [Range(1, 5)]
        public int ExperienceRating { get; set; }  // how great is it

        [Range(1, 5)]
        public int TechnicalRating { get; set; }   // how difficult is it technically

        [Range(1, 5)]
        public int EnduranceRating { get; set; }   // how strenuous is it

        [Range(1, 5)]
        [Display(Name = "Hard to Find")]
        public int ElusiveRating { get; set; }     // how hard is it to find

        [Display(Name = "Start at")]
        public Destination Start { get; set; }

        [Display(Name = "Reach")]
        public Destination End { get; set; }

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
        public ActivityDisp() { }

        public List<SelectDisp> StartActivities { get; set; }
        public List<SelectDisp> EndActivities { get; set; }
    }
}
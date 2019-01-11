using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Trip.Models
{
    public class SequenceData
    {
        public void CopyData(SequenceData s)
        {
            this.Name = s.Name;
        }

        [Key]
        public int SequenceID { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Sequence Name")]
        public string Name { get; set; }
    }

    public class Sequence : SequenceData
    {
        public virtual IList<SequenceToActivity> SequencesToActivities { get; set; }

        public double GetDuration()
        {
            double Duration = 0;
            for (int i = 0; i < SequencesToActivities.Count; i++)
            {
                Duration += SequencesToActivities[i].Activity.GetDuration();
            }
            return Duration;
        }
    }

    public class SequenceDisp : SequenceData
    {
        public SequenceDisp() { }
        public List<SelectDisp> Activities { get; set; }   // here Activities lead to Destinations, building a chain.
    }


    public class SequenceToActivity
    {
        public SequenceToActivity() { }
        public SequenceToActivity(int SequenceID, int ActivityID)
        {
            this.SequenceID = SequenceID;
            this.ActivityID = ActivityID;
        }
 
        public int SequenceToActivityID { get; set; }
        public int SequenceID { get; set; }
        public int ActivityID { get; set; }

        public virtual Sequence Sequence { get; set; }
        public virtual Activity Activity { get; set; }
    }


}
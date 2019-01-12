using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Trip.Models
{
  public class SequenceData
  {
    public void CopyData(SequenceData s)
    {
      this.SequenceID = s.SequenceID;
      this.Name = s.Name;
      this.RegionID = s.RegionID;
    }

    [Key]
    public int SequenceID { get; set; }

    [Required]
    [MaxLength(50)]
    [Display(Name = "Sequence Name")]
    public string Name { get; set; }

    [Required]
    public int RegionID { get; set; }
  }

  public class Sequence : SequenceData
  {
  }

  public class SequenceDisp : SequenceData
  {
    //    public SequenceDisp() { }

    public virtual Region Region { get; set; }
    //    public virtual IList<SequenceToActivity> SequencesToActivities { get; set; }

    public double GetDuration()
    {
      double Duration = 0;
      foreach (var item in SelectedActivities)
      {
        Duration += item.Activity.GetDuration();
      }
      return Duration;
    }

    public List<SequenceActivity> SelectedActivities { get; set; }
    public List<SequenceActivity> AvailableActivities { get; set; }
  }

  // helper class to contain one potential Activity for this sequence
  public class SequenceActivity
  {
    public SequenceActivity() { }
    public SequenceActivity(int id, int seqnr, Activity a, bool reverted)
    {
      this.Id = id;
      this.Seqnr = seqnr;
      this.Activity = a;
      this.Reverted = reverted;
    }

    public int Id { get; set; }
    public int Seqnr { get; set; }
    public Activity Activity { get; set; }
    public bool Reverted { get; set; }
    public Destination Dest1 { get; set; }
    public Destination Dest2 { get; set; }
  }

  public class SequenceToActivity
  {
    public SequenceToActivity() { }
    public SequenceToActivity(int SequenceID, int ActivityID, int seqnr, bool reverted)
    {
      this.SequenceID = SequenceID;
      this.ActivityID = ActivityID;
      this.Seqnr = seqnr;
      this.Reverted = reverted;
    }

    public int SequenceToActivityID { get; set; }
    public int SequenceID { get; set; }
    public int ActivityID { get; set; }
    public int Seqnr { get; set; }
    public bool Reverted { get; set; }

    public virtual Sequence Sequence { get; set; }
    public virtual Activity Activity { get; set; }
  }


}
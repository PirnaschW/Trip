using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Trip.Models
{
    public class VacationData
    {
        public void CopyData(VacationData s)
        {
            this.Name = s.Name;
        }
        
        [Key]
        public int VacationID { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Vacation")]
        public string Name { get; set; }

        //public enum Weekday { Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday };
        //[Required]
        //public Weekday Weekday { get; set; }
    }

    public class Vacation : VacationData
    {
        public virtual IList<VacationToSequence> VacationsToSequences { get; set; }

        public double GetDuration()
        {
            double Duration = 0;
            for (int i = 0; i < VacationsToSequences.Count; i++)
            {
//                Duration += VacationsToSequences[i].Sequence.GetDuration();
            }
            return Duration;
        }
    }

    public class VacationDisp : VacationData
    {
        public VacationDisp() { }
        public List<SelectDisp> Sequences { get; set; }
    }

    public class VacationToSequence
    {
        public VacationToSequence() { }
        public VacationToSequence(int VacationID, int SequenceID)
        {
            this.VacationID = VacationID;
            this.SequenceID = SequenceID;
        }

        public int VacationToSequenceID { get; set; }
        public int VacationID { get; set; }
        public int SequenceID { get; set; }

        public virtual Vacation Vacation { get; set; }
        public virtual Sequence Sequence { get; set; }
    }

}
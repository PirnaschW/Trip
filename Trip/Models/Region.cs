using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Trip.Models
{
    public class Region
    {
        [Key]
        public int RegionID { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Region")]
        public string Name { get; set; }

        public virtual IList<Destination> Destinations { get; set; }

    }
}
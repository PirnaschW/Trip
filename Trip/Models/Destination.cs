using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Trip.Models
{
  public class DestinationData
  {
    public void CopyData(DestinationData d)  // helper function
    {
      this.DestinationID = d.DestinationID;
      this.Name = d.Name;
      this.RegionID = d.RegionID;
      this.NatureRating = d.NatureRating;
      this.NightlifeRating = d.NightlifeRating;
      this.ShoppingRating = d.ShoppingRating;
    }

    [Key]
    public int DestinationID { get; set; }

    [Required]
    [MaxLength(50)]
    [Display(Name = "Destination")]
    public string Name { get; set; }

    [Required]
    public int RegionID { get; set; }

    [Required]
    [Range(0, 5)]
    public int NatureRating { get; set; }  // how worthy is it to go there for nature

    [Required]
    [Range(0, 5)]
    public int NightlifeRating { get; set; }  // how worthy is it to go there for partying

    [Required]
    [Range(0, 5)]
    public int ShoppingRating { get; set; }  // how worthy is it to go there for shopping
  }

  public class Destination : DestinationData
  {
  }

  public class DestinationDisp : DestinationData
  {
    public virtual Region Region { get; set; }
  }
}
        using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace ParkingLot.Models
{
    public class Vehicle : IModel
    {
        [Key]
        public int VehicleId { get; set; }


        [Display(Name = "Spot ID")]
        [AllowNull]
        public int SpotId { get; set; }
        //[Required]
        public string? VehicleModel { get; set; }
        [Display(Name = "Parking Name")]
        public string? ParkingName { get; set; }



    }
}

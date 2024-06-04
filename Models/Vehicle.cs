        using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingLot.Models
{
    public class Vehicle
    {
        [Key]
        public int VehicleId { get; set; }


        [ForeignKey("SpotId")]
        public int SpotId { get; set; }
        public string? VehicleModel { get; set; }



    }
}

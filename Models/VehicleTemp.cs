using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace ParkingLot.Models
{
    public class VehicleTemp : IModel
    {
        [Key]
        public int VehicleId { get; set; }

        public string? VehicleModel { get; set; }
 
        public string ParkingName { get; set; }



    }
}

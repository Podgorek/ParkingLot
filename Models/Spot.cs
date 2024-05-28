using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingLot.Models
{
    public class Spot
    {
        [Key]
        public int Id { get; set; }
        public int FloorId {  get; set; }

        [ForeignKey("FloorId")]
        public Floor Floor { get; set; }
        public bool IsOccupied { get; set; }

    }
}

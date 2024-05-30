using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingLot.Models
{
    public class Spot
    {
        [Key]
        public int SpotId { get; set; }
        public int FloorId {  get; set; }

        [ForeignKey("FloorId")]
        public Floor? Floor { get; set; }
        public bool IsOccupied { get; set; }
        public Vehicle? Vehicle { get; set; }

        public Spot(int floorId)
        {
            FloorId = floorId;
            IsOccupied = false;
        }
    }
}

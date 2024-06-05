using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingLot.Models
{
    public class Spot : IModel
    {
        [Key]
        public int SpotId { get; set; }
        [Required]
        public int SpotNumber { get; set; }
        public int ParkingId { get; set; }
        public int FloorId {  get; set; }
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

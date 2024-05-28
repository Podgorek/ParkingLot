using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingLot.Models
{
    public class Floor
    {
        [Key]
        public int FloorId { get; set; }
        public int ParkingId { get; set; }
        public int FloorLevel { get; set; }
        public int OccupiedSpotsCount { get; set; }
        public List<Spot> Spots { get; set; } = new List<Spot>();

    }
}

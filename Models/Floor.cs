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
        public int TotalSpots {  get; set; }
        public List<Spot> Spots { get; set; } = new ();
        public Floor(int parkingId, int floorLevel, int totalSpots)
        {
            ParkingId = parkingId;
            FloorLevel = floorLevel;
            TotalSpots = totalSpots;
            OccupiedSpotsCount = 0;
        }
    }
}

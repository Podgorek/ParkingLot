using System.ComponentModel.DataAnnotations;

namespace ParkingLot.Models
{
    public class Parking
    {
        [Key]
        public int ParkingId { get; set; }
        public int AllSpots { get; set; }
        public int FreeSpots { get; set; }
        public int NumberOfFloors { get; set; }
        public List<Floor> Floors { get; set; } = new List<Floor>();
        public Parking()
        {
            
        }
    }
}

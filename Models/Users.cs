using System.ComponentModel.DataAnnotations;

namespace ParkingLot.Models
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}

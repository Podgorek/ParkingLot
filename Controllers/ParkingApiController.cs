using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingLot.Data;
using ParkingLot.Models;

namespace ParkingLot.Controllers
{
    [Route("api/Parking")]
    [ApiController]
    public class ParkingApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ParkingApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Parking>>> GetParkings()
        {
            return await _context.Parkings.ToListAsync();
        }

        [HttpDelete("remove/{id}")]
        public async Task<ActionResult<IEnumerable<Parking>>> DeleteConfirmed(int id)
        {
            var parking = await _context.Parkings.FindAsync(id);

            if (parking != null)
            {
                _context.Parkings.Remove(parking);
            }

            await _context.SaveChangesAsync();

            return await _context.Parkings.ToListAsync();
        }
    }
}

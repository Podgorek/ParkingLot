using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingLot.Data;
using ParkingLot.Models;

namespace ParkingLot.Controllers
{
    [Route("api/Parkings")]
    [ApiController]
    public class ParkingApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ParkingApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("Index")]
        public async Task<ActionResult<IEnumerable<Parking>>> GetParkings()
        {
            return await _context.Parkings.ToListAsync();
        }

        [HttpGet("Details/{id}")]
        public async Task<ActionResult<Parking>> GetDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parking = await _context.Parkings
                .FirstOrDefaultAsync(p => p.ParkingId == id);

            if (parking == null)
            {
                return NotFound();
            }
            var viewModel = new Parking
            {
                ParkingId = parking.ParkingId,
                AllSpots = parking.AllSpots,
                FreeSpots = parking.FreeSpots,
                NumberOfFloors = parking.NumberOfFloors,
                Floors = _context.Floors.Where(f => f.ParkingId == parking.ParkingId).ToList()
            };

            return viewModel;

        }
        [HttpPost("Create")]
        public async Task<ActionResult<Parking>> Create([FromBody] Parking createdParking)
        {
            if (createdParking == null)
            {
                return BadRequest("Invalid parking object");
            }
            int spotsOnFloor = createdParking.AllSpots / createdParking.NumberOfFloors;
            int spotNum = 0;
            createdParking.FreeSpots = createdParking.AllSpots;
            _context.Parkings.Add(createdParking);
            await _context.SaveChangesAsync();
            for (int i = 0; i < createdParking.NumberOfFloors; i++)
            {
                var newFloor = new Floor(createdParking.ParkingId, i, spotsOnFloor);
                _context.Floors.Add(newFloor);
                await _context.SaveChangesAsync();

                for (int j = 0; j < spotsOnFloor; j++)
                {
                    var spot = new Spot(newFloor.FloorId)
                    {
                        SpotNumber = spotNum,
                        ParkingId = createdParking.ParkingId
                    };
                    _context.Spots.Add(spot);
                    spotNum++;
                }
            }
            var Wrongspot = _context.Spots.FirstOrDefault(s => s.ParkingId == 0);
            if( Wrongspot != null)
            {
                _context.Spots.Remove(Wrongspot);
            }
            var WrongFloor = _context.Floors.FirstOrDefault(f => f.TotalSpots == 0);
            if( WrongFloor != null)
            {
                _context.Floors.Remove(WrongFloor);
            }
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDetails), new { id = createdParking.ParkingId }, createdParking);
        }

        [HttpDelete("remove/{id}")]
        public async Task<ActionResult<IModel>> DeleteConfirmed(int id)
        {
            var parking = await _context.Parkings.FindAsync(id);

            if (parking != null)
            {
                _context.Parkings.Remove(parking);
            }
            else
                return new ErrorMessage
                {
                    Error = $"No vehicle with id {id}"
                };

            await _context.SaveChangesAsync();

            if (!_context.Parkings.Any(p => p.ParkingId == id))
            {
                return new ResponseDelete
                {
                    Success = true
                };
            }
            else
                return new ResponseDelete
                {
                    Success = false
                };
        }
    }
}

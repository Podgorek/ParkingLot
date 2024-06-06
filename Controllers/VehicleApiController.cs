using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingLot.Models;
using ParkingLot.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ParkingLot.Controllers
{
    [Route("api/Vehicles")]
    [ApiController]
    public class VehicleApiController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        public VehicleApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Vehicle/Index
        [HttpGet("Index")]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetVehicle()
        {
            return await _context.Vehicles.ToListAsync();
        }

        // GET api/<VehicleApiController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<VehicleApiController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<VehicleApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<VehicleApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

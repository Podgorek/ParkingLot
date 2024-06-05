using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ParkingLot.Data;
using ParkingLot.Models;
using System.Text.Json;
using System.Text;
using System.Collections;

namespace ParkingLot.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly ApplicationDbContext _context;

        private string _parkingName;
        private string _vehicleName;
        private int _vehicleId;
        private Vehicle _vehicle;

        public VehiclesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Vehicles
        public async Task<IActionResult> Index()
        {
/*            List<ExpandoObject> vehicleDetails = new();
            foreach (var vehicle in _context.Vehicles)
            {
                dynamic vehicleDetail = new ExpandoObject();
                vehicleDetail.Model = vehicle.VehicleModel;
                var floor = _context.Floors.Find(_context.Spots.Find(vehicle.SpotId).FloorId);
                vehicleDetail.Floor = floor.FloorLevel;
                vehicleDetail.Parking = floor.ParkingId;
                vehicleDetails.Add(vehicleDetail);
            }
            ViewBag.VehicleDetails = vehicleDetails;
 */           return View(await _context.Vehicles.ToListAsync());
        }

        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles
                .FirstOrDefaultAsync(m => m.VehicleId == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // GET: Vehicles/Create
        public IActionResult Create()
        {
            var parkingNames = _context.Parkings
                .Where(p => p.FreeSpots > 0)
                .Select(p => new SelectListItem
                {
                    Value = p.ParkingId.ToString(),
                    Text = p.ParkingName
                }).ToList();
            Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\nelo");
            ViewBag.ParkingName = parkingNames;

           /* var spotsId = _context.Spots
                .Where(s => s.IsOccupied == false)
                .Select(s => new SelectListItem
                {
                    Value = s.SpotId.ToString(),
                    Text = s.SpotNumber.ToString()
                })
                .ToList();

            ViewBag.SpotId = spotsId;*/

            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ParkingName,VehicleId,VehicleModel")] Vehicle vehicle)
        {
            _context.VehicleToCreate.Add(new VehicleTemp
            {
                VehicleId = vehicle.VehicleId,
                VehicleModel = vehicle.VehicleModel,
                ParkingName = vehicle.ParkingName
            });

            if (ModelState.IsValid)
            {
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(SpotChoose));
            }
            return View();
        }

        // GET: Vehicles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VehicleId,SpotId,VehicleModel")] Vehicle vehicle)
        {
            if (id != vehicle.VehicleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(vehicle.VehicleId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles
                .FirstOrDefaultAsync(m => m.VehicleId == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle != null)
            {
                var spot = _context.Spots.Find(vehicle.SpotId);
                spot.IsOccupied = false;
                var floor = _context.Floors.Find(spot.FloorId);
                floor.OccupiedSpotsCount--;
                var parking = _context.Parkings.Find(floor.ParkingId);
                parking.FreeSpots++;
                _context.Vehicles.Remove(vehicle);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleExists(int id)
        {
            return _context.Vehicles.Any(e => e.VehicleId == id);
        }

        // GET: Vehicles/SpotChoose/1
        [HttpGet]
        public async Task<IActionResult> SpotChoose()
        {
            var spot = await _context.Spots.FindAsync(_parkingName);
            Console.WriteLine("\n\n\n\n\n\n\n\n\n\n\n\\n\nn\n\n\n\\n\nelo elo");
            var spotsId = _context.Spots
                .Where(s => s.IsOccupied == false)
                .Select(s => new SelectListItem
                {
                    Value = s.SpotId.ToString(),
                    Text = s.SpotNumber.ToString()
                })
                .ToList();

            ViewBag.SpotId = spotsId;
            
            return View();
        }
        // POST: Vehicles/SpotChoose/2
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SpotChoose(int SpotId)
        {
            Console.WriteLine(SpotId);
            var spot = await _context.Spots.FindAsync(SpotId);

            if (spot == null)
            {
                return NotFound();
            }

            if (spot.IsOccupied)
            {
                return NotFound();
            }

            var floor = _context.Floors.Find(spot.FloorId);

            if (floor == null)
            {
                return NotFound();
            }

            var parking = _context.Parkings.Find(floor.ParkingId);

            if (parking == null)
            {
                return NotFound();
            }

            parking.FreeSpots--;
            floor.OccupiedSpotsCount++;
            spot.IsOccupied = true;

            

            var vehicle = new Vehicle
            {
                SpotId = SpotId,
                ParkingName = _context.VehicleToCreate.OrderBy(i => i.VehicleId).LastOrDefault().ParkingName,
                VehicleModel = _context.VehicleToCreate.OrderBy(i => i.VehicleId).LastOrDefault().VehicleModel,
            };

            

            ViewBag.SpotId = _context.Spots.Select(s => s.SpotId);

            if (ModelState.IsValid)
            {
                _context.Add(vehicle);
                _context.Update(parking);
                _context.Update(floor);
                _context.Update(spot);
                await _context.SaveChangesAsync();
                _vehicleName = string.Empty;
                _parkingName = string.Empty;
                _vehicle = null;
                return RedirectToAction(nameof(Index));
            }

            return View();
        }
    }

}

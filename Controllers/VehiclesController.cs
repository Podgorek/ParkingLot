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

        public VehiclesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Vehicles
        public async Task<IActionResult> Index()
        {
            return View(await _context.Vehicles.ToListAsync());
        }

        // GET: Vehicles/SearchVehicle
        public async Task<IActionResult> SearchVehicle()
        {
            return View();
        }

        // Post: Vehicles/Index/SortByModelAsc
        public IActionResult SortByModelAsc()
        {
            var sortedVehicles = _context.Vehicles.OrderBy(v => v.VehicleModel).ToList();
            return View("Index", sortedVehicles); 
        }

        // Post: Vehicles/Index/SortByModelAsc
        public IActionResult SortByModelDesc()
        {
            var sortedVehicles = _context.Vehicles.OrderByDescending(v => v.VehicleModel).ToList();
            return View("Index", sortedVehicles);
        }

        // Post: Vehicles/SearchResult
        public async Task<IActionResult> SearchResult(string SearchPhrase)
        {
            return View("Index", await _context.Vehicles.Where(v => v.VehicleModel.Contains(SearchPhrase)).ToListAsync());
        }

        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            const string FLOOR_EXCEPTION = "FloorNotFound";
            const string SPOT_EXCEPTION = "SpotNotFound";

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

            var spot = await _context.Spots
                 .FirstOrDefaultAsync(s => s.SpotId == vehicle.SpotId);

            if (spot == null)
            {
                ViewBag.SpotNum = SPOT_EXCEPTION;
                ViewBag.FloorLevel = FLOOR_EXCEPTION;
            }
            else
            {
                ViewBag.SpotNum = spot.SpotNumber;

                var floor = await _context.Floors
                    .FirstOrDefaultAsync(f => f.FloorId == spot.FloorId);
                ViewBag.FloorLevel = floor == null ? ViewBag.FloorLevel = FLOOR_EXCEPTION : floor.FloorLevel;
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

            ViewBag.ParkingName = parkingNames;


            return View();
        }

        // POST: Vehicles/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VehicleId,VehicleModel,ParkingName")] VehicleTemp vehicle)
        {

            _context.VehicleToCreate.Add(new VehicleTemp
            {
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
            
            var spotsId = await _context.Spots
                .Where(s => s.IsOccupied == false && Convert.ToInt32(_context.VehicleToCreate.OrderBy(i => i.VehicleId).LastOrDefault().ParkingName) == s.ParkingId)
                .Select(s => new SelectListItem
                {
                    Value = s.SpotId.ToString(),
                    Text = s.SpotNumber.ToString()
                })
                .ToListAsync();

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


            var parkingName = _context.Parkings.Find(Convert.ToInt32(_context.VehicleToCreate.OrderBy(i => i.VehicleId).LastOrDefault().ParkingName));
            var vehicle = new Vehicle
            {
                SpotId = SpotId,
                ParkingName = parkingName.ParkingName,
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
                return RedirectToAction(nameof(Index));
            }

            return View();
        }
    }

}

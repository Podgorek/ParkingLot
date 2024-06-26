﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using ParkingLot.ApiManager;
using ParkingLot.Data;
using ParkingLot.Models;

namespace ParkingLot.Controllers
{
    public class ParkingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ApiClient _client;
        const string URL = "https://localhost:7284/api/Parkings";

        public ParkingsController(ApplicationDbContext context)
        {
            _context = context;
            _client = new ApiClient();
        }


        // GET: Parkings
        public async Task<IActionResult> Index()
        {
            string url = $"{URL}/Index";
            
            return View(await _client.GetAsyncIEnumerable<List<Parking>>(url));
        }

        // GET: Parkings/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            string url = $"{URL}/Details/{id}";

            var result = await _client.GetAsync<Parking>(url);

            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        // GET: Parkings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Parkings/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ParkingId,NumberOfFloors,AllSpots,ParkingName")] Parking parking)
        {
            if (ModelState.IsValid)
            {
                string url = $"{URL}/Create";
                parking.FreeSpots = parking.AllSpots;

                var createdParking = await _client.PostAsyncDeserialized<Parking>(url, parking);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
                
                
            }

            return View(parking);
        }




        // GET: Parkings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }



            var parking = await _context.Parkings
                .FirstOrDefaultAsync(m => m.ParkingId == id);
            if (parking == null)
            {
                return NotFound();
            }

            return View(parking);
        }

        // POST: Parkings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string urlDelete = $"https://localhost:7284/api/Parkings/remove/{id}";

            var responseDelete = await _client.DeleteAsync<ResponseDelete>(urlDelete);

            if (responseDelete == null)
            {
                var responseError = await _client.DeleteAsync<ErrorMessage>(urlDelete);
                return View(responseError);
            }
            
            return responseDelete.Success ? RedirectToAction(nameof(Index)) : View();
        }

        private bool ParkingExists(int id)
        {
            return _context.Parkings.Any(e => e.ParkingId == id);
        }
    }
}

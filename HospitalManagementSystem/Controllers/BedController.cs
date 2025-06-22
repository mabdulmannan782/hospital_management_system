using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Controllers
{
    public class BedController : Controller
    {
        private readonly HospitalContext _context;

        public BedController(HospitalContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var beds = _context.Beds.Include(b => b.Room);
            return View(await beds.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewBag.Rooms = new SelectList(_context.Rooms, "RoomID", "RoomID");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Bed bed)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(bed);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","Bed");
            }
            ViewBag.Rooms = new SelectList(_context.Rooms, "RoomID", "RoomID", bed.RoomID);
            return View(bed);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var bed = await _context.Beds.FindAsync(id);
            if (bed == null) return NotFound();

            ViewBag.Rooms = new SelectList(_context.Rooms, "RoomID", "RoomID", bed.RoomID);
            return View(bed);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Bed bed)
        {
            if (id != bed.BedID) return NotFound();

            if (!ModelState.IsValid)
            {
                _context.Update(bed);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Bed");
            }
            ViewBag.Rooms = new SelectList(_context.Rooms, "RoomID", "RoomID", bed.RoomID);
            return View(bed);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var bed = await _context.Beds.Include(b => b.Room).FirstOrDefaultAsync(m => m.BedID == id);
            if (bed == null) return NotFound();
            return View(bed);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bed = await _context.Beds.FindAsync(id);
            if (bed != null) _context.Beds.Remove(bed);

            await _context.SaveChangesAsync();
            return RedirectToAction("Index","Bed");
        }
    }
}

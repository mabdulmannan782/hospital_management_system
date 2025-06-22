using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Controllers
{
    public class RoomController : Controller
    {
        private readonly HospitalContext _context;

        public RoomController(HospitalContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var rooms = _context.Rooms
                .Include(r => r.Hospital)
                .Include(r => r.Department);
            return View(await rooms.ToListAsync());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var room = await _context.Rooms
                .Include(r => r.Hospital)
                .Include(r => r.Department)
                .FirstOrDefaultAsync(m => m.RoomID == id);

            if (room == null)
                return NotFound();

            return View(room);
        }
        public IActionResult Create()
        {
            ViewBag.Hospitals = new SelectList(_context.Hospitals, "HospitalID", "Name");
            ViewBag.Departments = new SelectList(_context.Departments, "DepartmentID", "Name");
            return View();
        }

        // POST: Room/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Room room)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(room);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Room");
            }

            ViewBag.Hospitals = new SelectList(_context.Hospitals, "HospitalID", "Name", room.HospitalID);
            ViewBag.Departments = new SelectList(_context.Departments, "DepartmentID", "Name", room.DepartmentID);
            return View(room);
        }

        // GET: Room/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
                return NotFound();

            ViewBag.Hospitals = new SelectList(_context.Hospitals, "HospitalID", "Name", room.HospitalID);
            ViewBag.Departments = new SelectList(_context.Departments, "DepartmentID", "Name", room.DepartmentID);
            return View(room);
        }

        // POST: Room/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Room room)
        {
            if (id != room.RoomID)
                return NotFound();

            //if (ModelState.IsValid)
            //{
                try
                {
                    _context.Update(room);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomExists(room.RoomID))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction("Index","Room");
            //}

            ViewBag.Hospitals = new SelectList(_context.Hospitals, "HospitalID", "Name", room.HospitalID);
            ViewBag.Departments = new SelectList(_context.Departments, "DepartmentID", "Name", room.DepartmentID);
            return View(room);
        }

        // GET: Room/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var room = await _context.Rooms
                .Include(r => r.Hospital)
                .Include(r => r.Department)
                .FirstOrDefaultAsync(m => m.RoomID == id);

            if (room == null)
                return NotFound();

            return View(room);
        }

        // POST: Room/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var room =  _context.Rooms.Find(id);
            _context.Rooms.Remove(room);
             _context.SaveChanges();
            return RedirectToAction("Index","Room");
        }

        private bool RoomExists(int id)
        {
            return _context.Rooms.Any(e => e.RoomID == id);
        }
    }
}

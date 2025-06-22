using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Controllers
{
    public class ShiftController : Controller
    {
        private readonly HospitalContext _context;

        public ShiftController(HospitalContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var shifts = _context.Shifts.Include(s => s.Staff);
            return View(await shifts.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var shift = await _context.Shifts
                                      .Include(s => s.Staff)
                                      .FirstOrDefaultAsync(s => s.ShiftID == id);
            if (shift == null) return NotFound();

            return View(shift);
        }

        public IActionResult Create()
        {
            ViewBag.Staffs = new SelectList(_context.Staff, "StaffID", "FirstName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Shift shift)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(shift);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Staffs = new SelectList(_context.Staff, "StaffID", "FirstName", shift.StaffID);
            return View(shift);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var shift = await _context.Shifts.FindAsync(id);
            if (shift == null) return NotFound();

            ViewBag.Staffs = new SelectList(_context.Staff, "StaffID", "FirstName", shift.StaffID);
            return View(shift);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Shift shift)
        {
            if (id != shift.ShiftID) return NotFound();

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(shift);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Shifts.Any(s => s.ShiftID == id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Staffs = new SelectList(_context.Staff, "StaffID", "FirstName", shift.StaffID);
            return View(shift);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var shift = await _context.Shifts
                                      .Include(s => s.Staff)
                                      .FirstOrDefaultAsync(s => s.ShiftID == id);
            if (shift == null) return NotFound();

            return View(shift);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shift = await _context.Shifts.FindAsync(id);
            if (shift != null) _context.Shifts.Remove(shift);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

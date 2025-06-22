using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Controllers
{
    public class NurseController : Controller
    {
        private readonly HospitalContext _context;

        public NurseController(HospitalContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var nurses = _context.Nurses.Include(n => n.Department).Include(n => n.Hospital);
            return View(await nurses.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var nurse = await _context.Nurses
                                       .Include(n => n.Department)
                                       .Include(n => n.Hospital)
                                       .FirstOrDefaultAsync(n => n.NurseID == id);
            if (nurse == null) return NotFound();

            return View(nurse);
        }

        public IActionResult Create()
        {
            ViewBag.Departments = new SelectList(_context.Departments, "DepartmentID", "Name");
            ViewBag.Hospitals = new SelectList(_context.Hospitals, "HospitalID", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Nurse nurse)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(nurse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Departments = new SelectList(_context.Departments, "DepartmentID", "Name", nurse.DepartmentID);
            ViewBag.Hospitals = new SelectList(_context.Hospitals, "HospitalID", "Name", nurse.HospitalID);
            return View(nurse);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var nurse = await _context.Nurses.FindAsync(id);
            if (nurse == null) return NotFound();

            ViewBag.Departments = new SelectList(_context.Departments, "DepartmentID", "Name", nurse.DepartmentID);
            ViewBag.Hospitals = new SelectList(_context.Hospitals, "HospitalID", "Name", nurse.HospitalID);
            return View(nurse);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Nurse nurse)
        {
            if (id != nurse.NurseID) return NotFound();

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(nurse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Nurses.Any(n => n.NurseID == id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Departments = new SelectList(_context.Departments, "DepartmentID", "Name", nurse.DepartmentID);
            ViewBag.Hospitals = new SelectList(_context.Hospitals, "HospitalID", "Name", nurse.HospitalID);
            return View(nurse);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var nurse = await _context.Nurses
                                       .Include(n => n.Department)
                                       .Include(n => n.Hospital)
                                       .FirstOrDefaultAsync(n => n.NurseID == id);
            if (nurse == null) return NotFound();

            return View(nurse);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nurse = await _context.Nurses.FindAsync(id);
            if (nurse != null) _context.Nurses.Remove(nurse);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

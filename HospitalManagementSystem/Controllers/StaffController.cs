using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Controllers
{
    public class StaffController : Controller
    {
        private readonly HospitalContext _context;

        public StaffController(HospitalContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var staff = _context.Staff.Include(s => s.Department).Include(s => s.Hospital);
            return View(await staff.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var staff = await _context.Staff
                .Include(s => s.Department)
                .Include(s => s.Hospital)
                .FirstOrDefaultAsync(s => s.StaffID == id);
            if (staff == null) return NotFound();

            return View(staff);
        }

        public IActionResult Create()
        {
            ViewBag.Departments = new SelectList(_context.Departments, "DepartmentID", "Name");
            ViewBag.Hospitals = new SelectList(_context.Hospitals, "HospitalID", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Staff staff)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(staff);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Departments = new SelectList(_context.Departments, "DepartmentID", "Name", staff.DepartmentID);
            ViewBag.Hospitals = new SelectList(_context.Hospitals, "HospitalID", "Name", staff.HospitalID);
            return View(staff);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var staff = await _context.Staff.FindAsync(id);
            if (staff == null) return NotFound();

            ViewBag.Departments = new SelectList(_context.Departments, "DepartmentID", "Name", staff.DepartmentID);
            ViewBag.Hospitals = new SelectList(_context.Hospitals, "HospitalID", "Name", staff.HospitalID);
            return View(staff);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Staff staff)
        {
            if (id != staff.StaffID) return NotFound();

            if (!ModelState.IsValid)
            {
                _context.Update(staff);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Departments = new SelectList(_context.Departments, "DepartmentID", "Name", staff.DepartmentID);
            ViewBag.Hospitals = new SelectList(_context.Hospitals, "HospitalID", "Name", staff.HospitalID);
            return View(staff);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var staff = await _context.Staff
                .Include(s => s.Department)
                .Include(s => s.Hospital)
                .FirstOrDefaultAsync(s => s.StaffID == id);
            if (staff == null) return NotFound();

            return View(staff);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var staff = await _context.Staff.FindAsync(id);
            if (staff != null) _context.Staff.Remove(staff);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

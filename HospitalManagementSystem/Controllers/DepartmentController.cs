using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly HospitalContext _context;

        public DepartmentController(HospitalContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var departments = _context.Departments
                                      .Include(d => d.Hospital)
                                      .Include(d => d.Doctor);
            return View(await departments.ToListAsync());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var department = await _context.Departments
                                           .Include(d => d.Hospital)
                                           .Include(d => d.Doctor)
                                           .FirstOrDefaultAsync(d => d.DepartmentID == id);

            if (department == null) return NotFound();
            return View(department);
        }
        public IActionResult Create()
        {
            ViewBag.Hospitals = new SelectList(_context.Hospitals, "HospitalID", "Name");
            ViewBag.Doctors = new SelectList(_context.Doctors, "DoctorID", "FirstName");
            return View();
        }

        // POST: Departments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department department)
        {
            if (department.HospitalID == null || string.IsNullOrEmpty(department.Name))
            {
                ModelState.AddModelError("", "HospitalID and Name are required.");
                ViewBag.Hospitals = new SelectList(_context.Hospitals, "HospitalID", "Name", department.HospitalID);
                ViewBag.Doctors = new SelectList(_context.Doctors, "DoctorID", "FirstName", department.Head);
                return View(department);
            }

            // Proceed with saving to the database
            _context.Add(department);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Department");
            
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var department = await _context.Departments.FindAsync(id);
            if (department == null) return NotFound();

            ViewBag.Hospitals = new SelectList(_context.Hospitals, "HospitalID", "Name", department.HospitalID);
            ViewBag.Doctors = new SelectList(_context.Doctors, "DoctorID", "FirstName", department.Head);
            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Department department)
        {
            if (id != department.DepartmentID) return NotFound();

            if (ModelState.IsValid)
            {
                ViewBag.Hospitals = new SelectList(_context.Hospitals, "HospitalID", "Name", department.HospitalID);
                ViewBag.Doctors = new SelectList(_context.Doctors, "DoctorID", "FirstName", department.Head);
                return View(department);
            }

            try
            {
                _context.Update(department);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Departments.Any(d => d.DepartmentID == id))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToAction("Index", "Department");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var department = await _context.Departments
                                           .Include(d => d.Hospital)
                                           .Include(d => d.Doctor)
                                           .FirstOrDefaultAsync(d => d.DepartmentID == id);

            if (department == null) return NotFound();
            return View(department);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department != null)
                _context.Departments.Remove(department);

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Department");
        }
    }
}

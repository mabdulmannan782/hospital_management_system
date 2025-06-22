using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Controllers
{
    public class SurgeryController : Controller
    {
        private readonly HospitalContext _context;

        public SurgeryController(HospitalContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var surgeries = _context.Surgeries.Include(s => s.Patient).Include(s => s.Doctor);
            return View(await surgeries.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var surgery = await _context.Surgeries
                .Include(s => s.Patient)
                .Include(s => s.Doctor)
                .FirstOrDefaultAsync(s => s.SurgeryID == id);
            if (surgery == null) return NotFound();

            return View(surgery);
        }

        public IActionResult Create()
        {
            ViewBag.Patients = new SelectList(_context.Patient, "PatientID", "FirstName");
            ViewBag.Doctors = new SelectList(_context.Doctors, "DoctorID", "FirstName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Surgery surgery)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(surgery);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Patients = new SelectList(_context.Patient, "PatientID", "FirstName", surgery.PatientID);
            ViewBag.Doctors = new SelectList(_context.Doctors, "DoctorID", "FirstName", surgery.DoctorID);
            return View(surgery);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var surgery = await _context.Surgeries.FindAsync(id);
            if (surgery == null) return NotFound();

            ViewBag.Patients = new SelectList(_context.Patient, "PatientID", "FirstName", surgery.PatientID);
            ViewBag.Doctors = new SelectList(_context.Doctors, "DoctorID", "FirstName", surgery.DoctorID);
            return View(surgery);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Surgery surgery)
        {
            if (id != surgery.SurgeryID) return NotFound();

            if (!ModelState.IsValid)
            {
                _context.Update(surgery);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Patients = new SelectList(_context.Patient, "PatientID", "FirstName", surgery.PatientID);
            ViewBag.Doctors = new SelectList(_context.Doctors, "DoctorID", "FirstName", surgery.DoctorID);
            return View(surgery);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var surgery = await _context.Surgeries
                .Include(s => s.Patient)
                .Include(s => s.Doctor)
                .FirstOrDefaultAsync(s => s.SurgeryID == id);
            if (surgery == null) return NotFound();

            return View(surgery);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var surgery = await _context.Surgeries.FindAsync(id);
            if (surgery != null) _context.Surgeries.Remove(surgery);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

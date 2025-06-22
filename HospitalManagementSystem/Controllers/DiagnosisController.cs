using Microsoft.AspNetCore.Mvc;
using HospitalManagementSystem.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Controllers
{
    public class DiagnosisController : Controller
    {
        private readonly HospitalContext _context;

        public DiagnosisController(HospitalContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var diagnoses = _context.Diagnoses.Include(d => d.Patient).Include(d => d.Doctor);
            return View(await diagnoses.ToListAsync());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var diagnosis = await _context.Diagnoses
                                          .Include(d => d.Patient)
                                          .Include(d => d.Doctor)
                                          .FirstOrDefaultAsync(d => d.DiagnosisID == id);
            if (diagnosis == null) return NotFound();

            return View(diagnosis);
        }
        public IActionResult Create()
        {
            ViewBag.Patients = new SelectList(_context.Patient, "PatientID", "FirstName");
            ViewBag.Doctors = new SelectList(_context.Doctors, "DoctorID", "FirstName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Diagnosis diagnosis)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(diagnosis);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Diagnosis");
            }
            ViewBag.Patients = new SelectList(_context.Patient, "PatientID", "FirstName", diagnosis.PatientID);
            ViewBag.Doctors = new SelectList(_context.Doctors, "DoctorID", "FirstName", diagnosis.DoctorID);
            return View(diagnosis);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var diagnosis = await _context.Diagnoses.FindAsync(id);
            if (diagnosis == null) return NotFound();

            ViewBag.Patients = new SelectList(_context.Patient, "PatientID", "FirstName", diagnosis.PatientID);
            ViewBag.Doctors = new SelectList(_context.Doctors, "DoctorID", "FirstName", diagnosis.DoctorID);
            return View(diagnosis);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Diagnosis diagnosis)
        {
            if (id != diagnosis.DiagnosisID) return NotFound();

            if (!ModelState.IsValid)
            {
                _context.Update(diagnosis);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Diagnosis");
            }
            ViewBag.Patients = new SelectList(_context.Patient, "PatientID", "FirstName", diagnosis.PatientID);
            ViewBag.Doctors = new SelectList(_context.Doctors, "DoctorID", "FirstName", diagnosis.DoctorID);
            return View(diagnosis);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var diagnosis = await _context.Diagnoses
                .Include(d => d.Patient)
                .Include(d => d.Doctor)
                .FirstOrDefaultAsync(m => m.DiagnosisID == id);
            if (diagnosis == null) return NotFound();
            return View(diagnosis);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var diagnosis = await _context.Diagnoses.FindAsync(id);
            if (diagnosis != null) _context.Diagnoses.Remove(diagnosis);

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Diagnosis");
        }
    }
}

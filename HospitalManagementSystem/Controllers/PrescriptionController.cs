using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Controllers
{
    public class PrescriptionController : Controller
    {
        private readonly HospitalContext _context;

        public PrescriptionController(HospitalContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var prescriptions = _context.Prescriptions
                                        .Include(p => p.Patient)
                                        .Include(p => p.Doctor)
                                        .Include(p => p.Medication);
            return View(await prescriptions.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var prescription = await _context.Prescriptions
                                             .Include(p => p.Patient)
                                             .Include(p => p.Doctor)
                                             .Include(p => p.Medication)
                                             .FirstOrDefaultAsync(p => p.PrescriptionID == id);
            if (prescription == null) return NotFound();

            return View(prescription);
        }

        public IActionResult Create()
        {
            ViewBag.Patients = new SelectList(_context.Patient, "PatientID", "FirstName");
            ViewBag.Doctors = new SelectList(_context.Doctors, "DoctorID", "FirstName");
            ViewBag.Medications = new SelectList(_context.Medications, "MedicationID", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Prescription prescription)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(prescription);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Patients = new SelectList(_context.Patient, "PatientID", "FirstName", prescription.PatientID);
            ViewBag.Doctors = new SelectList(_context.Doctors, "DoctorID", "FirstName", prescription.DoctorID);
            ViewBag.Medications = new SelectList(_context.Medications, "MedicationID", "Name", prescription.MedicationID);
            return View(prescription);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var prescription = await _context.Prescriptions.FindAsync(id);
            if (prescription == null) return NotFound();

            ViewBag.Patients = new SelectList(_context.Patient, "PatientID", "FirstName", prescription.PatientID);
            ViewBag.Doctors = new SelectList(_context.Doctors, "DoctorID", "FirstName", prescription.DoctorID);
            ViewBag.Medications = new SelectList(_context.Medications, "MedicationID", "Name", prescription.MedicationID);
            return View(prescription);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Prescription prescription)
        {
            if (id != prescription.PrescriptionID) return NotFound();

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(prescription);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Prescriptions.Any(p => p.PrescriptionID == id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Patients = new SelectList(_context.Patient, "PatientID", "FirstName", prescription.PatientID);
            ViewBag.Doctors = new SelectList(_context.Doctors, "DoctorID", "FirstName", prescription.DoctorID);
            ViewBag.Medications = new SelectList(_context.Medications, "MedicationID", "Name", prescription.MedicationID);
            return View(prescription);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var prescription = await _context.Prescriptions
                                             .Include(p => p.Patient)
                                             .Include(p => p.Doctor)
                                             .Include(p => p.Medication)
                                             .FirstOrDefaultAsync(p => p.PrescriptionID == id);
            if (prescription == null) return NotFound();

            return View(prescription);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prescription = await _context.Prescriptions.FindAsync(id);
            if (prescription != null) _context.Prescriptions.Remove(prescription);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

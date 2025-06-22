using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Controllers
{
    public class AppointmentController : Controller
    {

        private readonly HospitalContext _context;

        public AppointmentController(HospitalContext context)
        {
            _context = context;
        }

        // GET: Appointments
        public async Task<IActionResult> Index()
        {
            var appointments = _context.Appointments
                                       .Include(b => b.Patient)
                                       .Include(b => b.Doctor);
            return View(await appointments.ToListAsync());
        }

        // GET: Appointments/Create
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var appointment = await _context.Appointments
                                            .Include(a => a.Patient)
                                            .Include(a => a.Doctor)
                                            .FirstOrDefaultAsync(a => a.AppointmentID == id);

            if (appointment == null) return NotFound();
            return View(appointment);
        }
        public IActionResult Create()
        {
            ViewBag.Patients = new SelectList(_context.Patient, "PatientID", "FirstName");
            ViewBag.Doctors = new SelectList(_context.Doctors, "DoctorID", "FirstName");
            return View();
        }

        // POST: Appointments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Appointment appointment)
        {
            //if (ModelState.IsValid)
            //{
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","Appointment");
            //}
            ViewBag.Patients = new SelectList(_context.Patient, "PatientID", "FirstName", appointment.PatientID);
            ViewBag.Doctors = new SelectList(_context.Doctors, "DoctorID", "FirstName", appointment.DoctorID);
            return View(appointment);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null) return NotFound();

            ViewBag.Patients = new SelectList(_context.Patient, "PatientID", "FirstName", appointment.PatientID);
            ViewBag.Doctors = new SelectList(_context.Doctors, "DoctorID", "FirstName", appointment.DoctorID);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Appointment appointment)
        {
            if (id != appointment.AppointmentID) return NotFound();

            //if (ModelState.IsValid)
            //{
                try
                {
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Appointments.Any(e => e.AppointmentID == id)) return NotFound();
                    else throw;
                }
                return RedirectToAction("Index", "Appointment");
            //}
            ViewBag.Patients = new SelectList(_context.Patient, "PatientID", "FirstName", appointment.PatientID);
            ViewBag.Doctors = new SelectList(_context.Doctors, "DoctorID", "FirstName", appointment.DoctorID);
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var appointment = await _context.Appointments
                                            .Include(a => a.Patient)
                                            .Include(a => a.Doctor)
                                            .FirstOrDefaultAsync(a => a.AppointmentID == id);

            if (appointment == null) return NotFound();
            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Appointment");
        }
    }
}

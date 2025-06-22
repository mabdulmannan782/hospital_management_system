using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace HospitalManagementSystem.Controllers
{
    public class PatientController : Controller
    {
        private readonly HospitalContext _context;
        public PatientController(HospitalContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var patients = _context.Patient.ToList();
            return View(patients);
        }
        public IActionResult Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var patient = _context.Patient.FirstOrDefault(p => p.PatientID == id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }
        // GET: Patient/Create
        public IActionResult Create()
        {
            ViewBag.HospitalID = new SelectList(_context.Hospitals, "HospitalID", "Name");
            return View();
        }

        // POST: Patient/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Patient patient)
        {
            _context.Patient.Add(patient);
            _context.SaveChanges();
            return RedirectToAction("Index","Patient");
            return View(patient);
        }

        // GET: Patient/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var patient = _context.Patient.Find(id);
            if (patient == null)
                return NotFound();
            ViewBag.HospitalID = new SelectList(_context.Hospitals, "HospitalID", "Name", patient.HospitalID);
            return View(patient);
        }

        // POST: Patient/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Patient patient)
        {
            if (id != patient.PatientID)
                return NotFound();

            
            _context.Update(patient);
            _context.SaveChanges();
            return RedirectToAction("Index","Patient");
            return View(patient);
        }

        // GET: Patient/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var patient = _context.Patient.FirstOrDefault(p => p.PatientID == id);
            if (patient == null)
                return NotFound();

            return View(patient);
        }

        // POST: Patient/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var patient = _context.Patient.Find(id);
            _context.Patient.Remove(patient);
            _context.SaveChanges();
            return RedirectToAction("Index", "Patient");
        }
    }
}

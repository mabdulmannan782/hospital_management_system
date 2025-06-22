using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Controllers
{
    public class DoctorController : Controller
    {
        private readonly HospitalContext _context;

        public DoctorController(HospitalContext context)
        {
            _context = context;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
        // GET: Doctor
        public async Task<IActionResult> Index()
        {
            ViewBag.Doctor = _context.Doctors
                            .Include(d => d.Hospital)   // Include Hospital navigation property
                            .Include(d => d.Department);
            return View(ViewBag.Doctor);
        }

        // GET: Doctor/Create
        public IActionResult Create()
        {
            ViewBag.HospitalID = new SelectList(_context.Hospitals, "HospitalID", "Name");
            ViewBag.DepartmentID = new SelectList(_context.Departments, "DepartmentID", "Name");
            return View();
        }

        // POST: Doctor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Doctor doctor)
        {
            //if (ModelState.IsValid)
            //{
                _context.Add(doctor);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","Doctor");
            //}
                ViewBag.HospitalID = new SelectList(_context.Hospitals, "HospitalID", "Name", doctor.HospitalID);
                ViewBag.DepartmentID = new SelectList(_context.Departments, "DepartmentID", "Name", doctor.DepartmentID);
                return View(doctor);

        }

        // GET: Doctor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
                return NotFound();

            ViewBag.HospitalID = new SelectList(_context.Hospitals, "HospitalID", "Name", doctor.HospitalID);
            ViewBag.DepartmentID = new SelectList(_context.Departments, "DepartmentID", "Name", doctor.DepartmentID);
            return View(doctor);
        }

        // POST: Doctor/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Doctor doctor)
        {
            if (id != doctor.DoctorID)
                return NotFound();

            //if (ModelState.IsValid)
            //{
                _context.Update(doctor);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Doctor");

            //}
            

            ViewBag.HospitalID = new SelectList(_context.Hospitals, "HospitalID", "Name", doctor.HospitalID);
            ViewBag.DepartmentID = new SelectList(_context.Departments, "DepartmentID", "Name", doctor.DepartmentID);
            return View(doctor);
        }

        // GET: Doctor/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var doctor =  _context.Doctors
                .Include(d => d.Hospital)
                .Include(d => d.Department)
                .FirstOrDefault(m => m.DoctorID == id);

            if (doctor == null)
                return NotFound();

            return View(doctor);
        }

        // POST: Doctor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var doctor = _context.Doctors.Find(id);
           
                _context.Doctors.Remove(doctor);
                _context.SaveChanges();
            

            return RedirectToAction("Index", "Doctor");
        }
    }
}

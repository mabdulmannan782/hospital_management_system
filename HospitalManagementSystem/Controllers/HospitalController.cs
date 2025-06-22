using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Controllers
{
    public class HospitalController : Controller
    {
        private readonly HospitalContext _context;

        public HospitalController(HospitalContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var hospitals = _context.Hospitals.ToList();
            return View(hospitals);
        }
        public IActionResult Create()
        {
            return View();
        }

        // POST: Hospital/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Hospital hospital)
        {
            //if (ModelState.IsValid)
            //{

            //}
            _context.Hospitals.Add(hospital);
            _context.SaveChanges();
            return RedirectToAction("Index","Hospital");
            return View(hospital);
        }
        public IActionResult Edit(int id)
        {
            var hospital = _context.Hospitals.Find(id);
            if (hospital == null)
            {
                return NotFound();
            }
            return View(hospital);
        }

        // POST: Hospital/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Hospital hospital)
        {
            if (id != hospital.HospitalID)
                return NotFound();

            
                _context.Update(hospital);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            
            return View(hospital);
        }

        // GET: Hospital/Delete/5
        public IActionResult Delete(int id)
        {
            var hospital = _context.Hospitals.Find(id);
            if (hospital == null)
            {
                return NotFound();
            }
            return View(hospital);
        }

        // POST: Hospital/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var hospital = _context.Hospitals.Find(id);
            if (hospital != null)
            {
                _context.Hospitals.Remove(hospital);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

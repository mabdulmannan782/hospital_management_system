using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Controllers
{
    public class VisitorController : Controller
    {
        private readonly HospitalContext _context;

        public VisitorController(HospitalContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var visitors = _context.Visitors
                .Include(v => v.Patient);
            return View(await visitors.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var visitor = await _context.Visitors
                .Include(v => v.Patient)
                .FirstOrDefaultAsync(v => v.VisitorID == id);

            if (visitor == null) return NotFound();
            return View(visitor);
        }

        public IActionResult Create()
        {
            ViewBag.Patients = new SelectList(_context.Patient, "PatientID", "FirstName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Visitor visitor)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(visitor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Patients = new SelectList(_context.Patient, "PatientID", "FirstName", visitor.PatientID);
            return View(visitor);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var visitor = await _context.Visitors.FindAsync(id);
            if (visitor == null) return NotFound();

            ViewBag.Patients = new SelectList(_context.Patient, "PatientID", "FirstName", visitor.PatientID);
            return View(visitor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Visitor visitor)
        {
            if (id != visitor.VisitorID) return NotFound();

            if (!ModelState.IsValid)
            {
                _context.Update(visitor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Patients = new SelectList(_context.Patient, "PatientID", "FirstName", visitor.PatientID);
            return View(visitor);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var visitor = await _context.Visitors
                .Include(v => v.Patient)
                .FirstOrDefaultAsync(v => v.VisitorID == id);

            if (visitor == null) return NotFound();
            return View(visitor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var visitor = await _context.Visitors.FindAsync(id);
            if (visitor != null) _context.Visitors.Remove(visitor);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

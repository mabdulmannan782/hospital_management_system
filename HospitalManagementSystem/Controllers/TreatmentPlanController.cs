using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Controllers
{
    public class TreatmentPlanController : Controller
    {
        private readonly HospitalContext _context;

        public TreatmentPlanController(HospitalContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var treatmentPlans = _context.TreatmentPlans
                .Include(tp => tp.Diagnosis);
            return View(await treatmentPlans.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var treatmentPlan = await _context.TreatmentPlans
                .Include(tp => tp.Diagnosis)
                .FirstOrDefaultAsync(tp => tp.TreatmentPlanID == id);

            if (treatmentPlan == null) return NotFound();
            return View(treatmentPlan);
        }

        public IActionResult Create()
        {
            ViewBag.Diagnoses = new SelectList(_context.Diagnoses, "DiagnosisID", "Description");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TreatmentPlan treatmentPlan)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(treatmentPlan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Diagnoses = new SelectList(_context.Diagnoses, "DiagnosisID", "Description", treatmentPlan.DiagnosisID);
            return View(treatmentPlan);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var treatmentPlan = await _context.TreatmentPlans.FindAsync(id);
            if (treatmentPlan == null) return NotFound();

            ViewBag.Diagnoses = new SelectList(_context.Diagnoses, "DiagnosisID", "Description", treatmentPlan.DiagnosisID);
            return View(treatmentPlan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TreatmentPlan treatmentPlan)
        {
            if (id != treatmentPlan.TreatmentPlanID) return NotFound();

            if (!ModelState.IsValid)
            {
                _context.Update(treatmentPlan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Diagnoses = new SelectList(_context.Diagnoses, "DiagnosisID", "Description", treatmentPlan.DiagnosisID);
            return View(treatmentPlan);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var treatmentPlan = await _context.TreatmentPlans
                .Include(tp => tp.Diagnosis)
                .FirstOrDefaultAsync(tp => tp.TreatmentPlanID == id);

            if (treatmentPlan == null) return NotFound();
            return View(treatmentPlan);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var treatmentPlan = await _context.TreatmentPlans.FindAsync(id);
            if (treatmentPlan != null) _context.TreatmentPlans.Remove(treatmentPlan);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

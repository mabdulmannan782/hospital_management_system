using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
namespace HospitalManagementSystem.Controllers
{
    public class InsuranceController : Controller
    {
        public readonly HospitalContext _context;
        public InsuranceController(HospitalContext context)
        {
            this._context = context;
        }
        // GET: Insurance
        public async Task<IActionResult> Index()
        {
            var insurance = _context.Insurances.Include(i => i.Patient);
            return View(await insurance.ToListAsync());
        }

        // GET: Insurance/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var insurance = await _context.Insurances
                .Include(i => i.Patient)
                .FirstOrDefaultAsync(m => m.InsuranceID == id);

            if (insurance == null) return NotFound();

            return View(insurance);
        }

        // GET: Insurance/Create
        public IActionResult Create()
        {
            ViewBag.PatientID = new SelectList(_context.Patient, "PatientID", "FirstName");
            return View();
        }

        // POST: Insurance/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InsuranceID,PatientID,Provider,PolicyNumber,CoverageDetails")] Insurance insurance)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(insurance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.PatientID = new SelectList(_context.Patient, "PatientID", "FirstName", insurance.PatientID);
            return View(insurance);
        }

        // GET: Insurance/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var insurance = await _context.Insurances.FindAsync(id);
            if (insurance == null) return NotFound();

            ViewBag.PatientID = new SelectList(_context.Patient, "PatientID", "FirstName", insurance.PatientID);
            return View(insurance);
        }

        // POST: Insurance/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InsuranceID,PatientID,Provider,PolicyNumber,CoverageDetails")] Insurance insurance)
        {
            if (id != insurance.InsuranceID) return NotFound();

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(insurance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsuranceExists(insurance.InsuranceID)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.PatientID = new SelectList(_context.Patient, "PatientID", "FirstName", insurance.PatientID);
            return View(insurance);
        }

        // GET: Insurance/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var insurance = await _context.Insurances
                .Include(i => i.Patient)
                .FirstOrDefaultAsync(m => m.InsuranceID == id);

            if (insurance == null) return NotFound();

            return View(insurance);
        }

        // POST: Insurance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var insurance = await _context.Insurances.FindAsync(id);
            if (insurance != null)
            {
                _context.Insurances.Remove(insurance);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool InsuranceExists(int id)
        {
            return _context.Insurances.Any(e => e.InsuranceID == id);
        }
    }
}

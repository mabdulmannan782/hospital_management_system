using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Controllers
{
    public class TestResultController : Controller
    {
        private readonly HospitalContext _context;

        public TestResultController(HospitalContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var testResults = _context.TestResults
                .Include(tr => tr.LabTest)
                .Include(tr => tr.Patient);
            return View(await testResults.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var testResult = await _context.TestResults
                .Include(tr => tr.LabTest)
                .Include(tr => tr.Patient)
                .FirstOrDefaultAsync(tr => tr.TestResultID == id);
            if (testResult == null) return NotFound();

            return View(testResult);
        }

        public IActionResult Create()
        {
            ViewBag.LabTests = new SelectList(_context.LabTests, "LabTestID", "Name");
            ViewBag.Patients = new SelectList(_context.Patient, "PatientID", "FirstName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TestResult testResult)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(testResult);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.LabTests = new SelectList(_context.LabTests, "LabTestID", "Name", testResult.LabTestID);
            ViewBag.Patients = new SelectList(_context.Patient, "PatientID", "FirstName", testResult.PatientID);
            return View(testResult);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var testResult = await _context.TestResults.FindAsync(id);
            if (testResult == null) return NotFound();

            ViewBag.LabTests = new SelectList(_context.LabTests, "LabTestID", "Name", testResult.LabTestID);
            ViewBag.Patients = new SelectList(_context.Patient, "PatientID", "FirstName", testResult.PatientID);
            return View(testResult);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TestResult testResult)
        {
            if (id != testResult.TestResultID) return NotFound();

            if (!ModelState.IsValid)
            {
                _context.Update(testResult);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.LabTests = new SelectList(_context.LabTests, "LabTestID", "Name", testResult.LabTestID);
            ViewBag.Patients = new SelectList(_context.Patient, "PatientID", "FirstName", testResult.PatientID);
            return View(testResult);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var testResult = await _context.TestResults
                .Include(tr => tr.LabTest)
                .Include(tr => tr.Patient)
                .FirstOrDefaultAsync(tr => tr.TestResultID == id);
            if (testResult == null) return NotFound();

            return View(testResult);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var testResult = await _context.TestResults.FindAsync(id);
            if (testResult != null) _context.TestResults.Remove(testResult);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

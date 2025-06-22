using Microsoft.AspNetCore.Mvc;
using HospitalManagementSystem.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HospitalManagementSystem.Models;

namespace HospitalManagementSystem.Controllers
{
    public class LabTestController : Controller
    {
        private readonly HospitalContext _context;

        public LabTestController(HospitalContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.LabTests.ToListAsync());
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LabTest labTest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(labTest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(labTest);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var labTest = await _context.LabTests.FindAsync(id);
            if (labTest == null) return NotFound();

            return View(labTest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LabTest labTest)
        {
            if (id != labTest.LabTestID) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(labTest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(labTest);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var labTest = await _context.LabTests.FindAsync(id);
            if (labTest == null) return NotFound();

            return View(labTest);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var labTest = await _context.LabTests.FindAsync(id);
            if (labTest != null) _context.LabTests.Remove(labTest);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

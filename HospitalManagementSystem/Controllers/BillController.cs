using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Controllers
{
    public class BillController : Controller
    {
        private readonly HospitalContext _context;

        public BillController(HospitalContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var bills = _context.Bills.Include(b => b.Patient);
            return View(await bills.ToListAsync());
        }
        public async Task<IActionResult> Details(int id)
        {
            var bill = await _context.Bills
                .Include(b => b.Patient)
                .FirstOrDefaultAsync(m => m.BillID == id);

            if (bill == null) return NotFound();

            return View(bill);
        }
        public IActionResult Create()
        {
            ViewBag.Patients = new SelectList(_context.Patient, "PatientID", "FirstName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Bill bill)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(bill);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","Bill");
            }
            ViewBag.Patients = new SelectList(_context.Patient, "PatientID", "FirstName", bill.PatientID);
            return View(bill);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var bill = await _context.Bills.FindAsync(id);
            if (bill == null) return NotFound();

            ViewBag.Patients = new SelectList(_context.Patient, "PatientID", "FirstName", bill.PatientID);
            return View(bill);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Bill bill)
        {
            if (id != bill.BillID) return NotFound();

            if (!ModelState.IsValid)
            {
                _context.Update(bill);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Bill");
            }
            ViewBag.Patients = new SelectList(_context.Patient, "PatientID", "FirstName", bill.PatientID);
            return View(bill);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var bill = await _context.Bills.Include(b => b.Patient).FirstOrDefaultAsync(m => m.BillID == id);
            if (bill == null) return NotFound();
            return View(bill);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bill = await _context.Bills.FindAsync(id);
            if (bill != null) _context.Bills.Remove(bill);

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Bill");
        }
    }
}

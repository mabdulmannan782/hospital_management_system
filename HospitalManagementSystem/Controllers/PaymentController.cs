using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Controllers
{
    public class PaymentController : Controller
    {
        private readonly HospitalContext _context;

        public PaymentController(HospitalContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var payments = _context.Payments.Include(p => p.Bill);
            return View(await payments.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var payment = await _context.Payments
                                        .Include(p => p.Bill)
                                        .FirstOrDefaultAsync(p => p.PaymentID == id);
            if (payment == null) return NotFound();

            return View(payment);
        }

        public IActionResult Create()
        {
            ViewBag.Bills = new SelectList(_context.Bills, "BillID", "BillID");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Payment payment)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(payment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Bills = new SelectList(_context.Bills, "BillID", "BillID", payment.BillID);
            return View(payment);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null) return NotFound();

            ViewBag.Bills = new SelectList(_context.Bills, "BillID", "BillID", payment.BillID);
            return View(payment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Payment payment)
        {
            if (id != payment.PaymentID) return NotFound();

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(payment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Payments.Any(p => p.PaymentID == id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Bills = new SelectList(_context.Bills, "BillID", "BillID", payment.BillID);
            return View(payment);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var payment = await _context.Payments
                                        .Include(p => p.Bill)
                                        .FirstOrDefaultAsync(p => p.PaymentID == id);
            if (payment == null) return NotFound();

            return View(payment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment != null) _context.Payments.Remove(payment);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

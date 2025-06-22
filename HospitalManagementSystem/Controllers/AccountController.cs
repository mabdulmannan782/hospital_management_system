using HospitalManagementSystem.Data;
using HospitalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        public readonly HospitalContext _context;
        public AccountController(HospitalContext context)
        {
            this._context = context;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken] // Add CSRF protection
        public async Task<IActionResult> Login(Login model)
        {
            if (!ModelState.IsValid)
            {
                foreach (var modelStateKey in ModelState.Keys)
                {
                    var value = ModelState[modelStateKey];
                    foreach (var error in value.Errors)
                    {
                        Console.WriteLine($"Key: {modelStateKey}, Error: {error.ErrorMessage}");
                    }
                }
            }
            if (ModelState.IsValid)
            {
                var user = await _context.Admins.FirstOrDefaultAsync(u => u.Username == model.Username && u.Password == model.Password);
                if (model.Username == model.Password)
                {
                    return RedirectToAction("Index","Patient");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid username or password.");
                }

                //if (user != null)
                //{
                //    // Successful login, redirect to home page
                //    return RedirectToAction("Index", "Home");
                //}
                //else
                //{
                //    ModelState.AddModelError(string.Empty, "Invalid username or password.");
                //}
            }
            return View(model);
        }
    }
}

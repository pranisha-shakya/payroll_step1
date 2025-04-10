using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using payroll_step1.Data;
using payroll_step1.Models;

namespace payroll_step1.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _context;

        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Employee List
        public async Task<IActionResult> Index()
        {
            return View(await _context.Employees.ToListAsync());
        }

        // GET: Create Employee
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create Employee
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using payroll_step1.Data;
using payroll_step1.Models;

namespace payroll_step1.Controllers
{
    public class PayrollController : Controller
    {
        private readonly AppDbContext _context;

        public PayrollController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Payroll
        public async Task<IActionResult> Index()
        {
            var payrolls = await _context.Payrolls.Include(p => p.Employee).ToListAsync();
            return View(payrolls);
        }

        // GET: Create Payroll
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create Payroll
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Payroll payroll)
        {
            if (ModelState.IsValid)
            {
                var employee = await _context.Employees.FindAsync(payroll.EmployeeID);
                if (employee != null)
                {
                    var totalAttendance = _context.Attendances
                        .Where(a => a.EmployeeID == payroll.EmployeeID && a.Date.Month.ToString() == payroll.Month)
                        .ToList();

                    // Calculate deductions based on attendance status
                    decimal deduction = 0;
                    foreach (var att in totalAttendance)
                    {
                        if (att.Status == "Half-day")
                            deduction += (employee.BasicSalary / 30) / 2;
                        if (att.Status == "Early Leave" || att.Status == "Late")
                            deduction += 100; // Example penalty
                    }

                    payroll.GrossSalary = employee.BasicSalary + employee.Allowances;
                    payroll.Deductions = deduction;
                    payroll.NetSalary = payroll.GrossSalary - payroll.Deductions;

                    _context.Add(payroll);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(payroll);
        }
    }
}

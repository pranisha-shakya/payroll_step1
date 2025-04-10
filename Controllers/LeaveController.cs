using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using payroll_step1.Data;
using payroll_step1.Models;

namespace payroll_step1.Controllers
{
    public class LeaveController : Controller
    {
        private readonly AppDbContext _context;

        public LeaveController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Leave List
        public async Task<IActionResult> Index()
        {
            var leaves = await _context.Leaves.Include(l => l.Employee).ToListAsync();
            return View(leaves);
        }

        // GET: Apply Leave
        public IActionResult Create()
        {
            return View();
        }

        // POST: Apply Leave
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Leave leave)
        {
            if (ModelState.IsValid)
            {
                _context.Add(leave);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(leave);
        }

        // Approve Leave
        public async Task<IActionResult> Approve(int id)
        {
            var leave = await _context.Leaves.FindAsync(id);
            if (leave != null)
            {
                leave.Status = "Approved";
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

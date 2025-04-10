using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using payroll_step1.Data;
using payroll_step1.Models;

namespace payroll_step1.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly AppDbContext _context;
        private readonly TimeSpan _thresholdIn = new TimeSpan(9, 0, 0);  // 9:00 AM
        private readonly TimeSpan _thresholdOut = new TimeSpan(17, 0, 0); // 5:00 PM

        public AttendanceController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Attendance
        public async Task<IActionResult> Index()
        {
            var attendanceList = await _context.Attendances.Include(a => a.Employee).ToListAsync();
            return View(attendanceList);
        }

        // GET: Create Attendance
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create Attendance
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Attendance attendance)
        {
            if (ModelState.IsValid)
            {
                if (attendance.CheckInTime.HasValue && attendance.CheckOutTime.HasValue)
                {
                    var totalHours = (attendance.CheckOutTime.Value - attendance.CheckInTime.Value).TotalHours;
                    attendance.HoursWorked = Math.Max(0, totalHours);

                    // Determine status based on thresholds
                    if (attendance.CheckInTime < _thresholdIn)
                        attendance.Status = "Early";
                    else if (attendance.CheckInTime > _thresholdIn)
                        attendance.Status = "Late";

                    if (attendance.CheckOutTime < _thresholdOut)
                        attendance.Status = "Early Leave";
                    else
                        attendance.Status = attendance.HoursWorked >= 8 ? "Full-day" : "Half-day";
                }

                _context.Add(attendance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(attendance);
        }
        public IActionResult Edit(int id)
        {
            var attendance = _context.Attendances.FirstOrDefault(a => a.AttendanceID == id);
            if (attendance == null)
            {
                return NotFound(); // Returns 404 if no record is found
            }
            return View(attendance);
        }
    }

}

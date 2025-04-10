using System.ComponentModel.DataAnnotations;

namespace payroll_step1.Models
{
    public class Attendance
    {
        [Key]
        public int AttendanceID { get; set; }

        public int EmployeeID { get; set; }
        public Employee Employee { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public TimeSpan? CheckInTime { get; set; }
        public TimeSpan? CheckOutTime { get; set; }

        public double HoursWorked { get; set; } = 0;

        public string Status { get; set; }
    }
}

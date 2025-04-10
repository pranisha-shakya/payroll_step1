using System.ComponentModel.DataAnnotations;

namespace payroll_step1.Models
{
    public class Leave
    {
        [Key]
        public int LeaveID { get; set; }

        public int EmployeeID { get; set; }
        public Employee Employee { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string LeaveType { get; set; } // Sick Leave, Casual Leave, etc.

        public string Status { get; set; } = "Pending"; // Pending, Approved, Rejected
    }
}

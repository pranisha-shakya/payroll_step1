using System.ComponentModel.DataAnnotations;

namespace payroll_step1.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Designation { get; set; }

        [Required]
        public decimal BasicSalary { get; set; }

        public decimal Allowances { get; set; }

        public ICollection<Attendance> Attendances { get; set; }
        public ICollection<Payroll> Payrolls { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace payroll_step1.Models
{
    public class Payroll
    {
        [Key]
        public int PayrollID { get; set; }

        public int EmployeeID { get; set; }
        public Employee Employee { get; set; }

        [Required]
        public string Month { get; set; }

        [DataType(DataType.Currency)]
        public decimal GrossSalary { get; set; }

        [DataType(DataType.Currency)]
        public decimal Deductions { get; set; }

        [DataType(DataType.Currency)]
        public decimal NetSalary { get; set; }
    }
}

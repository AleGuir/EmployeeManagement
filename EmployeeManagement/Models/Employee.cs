using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
{
    public class Employee
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "The name is obligatory")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The position is obligatory")]
        public string Position { get; set; }

        [Required(ErrorMessage = "The Office is Obligatory")]
        public string Office { get; set; }

        [Required(ErrorMessage = "The salary is obligatory")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Salary must be a positive value")]
        public decimal Salary { get; set; }


    }
}
using System.ComponentModel.DataAnnotations;

namespace EmployeeRegistrationAPI.Models
{
    public class Register
    {
        [Key]
        public int Empid { get; set; }
        [Required]
        public string? Empname { get; set; }

        [Required(ErrorMessage = "Age required")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Email required")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Phone number is required")]
      
        public string? Phno { get; set; }
        [Required(ErrorMessage = "Address required")]
        public string? Address { get; set; }
        [Required(ErrorMessage = "Gender required")]
        public string? Gender { get; set; }
        [Required(ErrorMessage = "Desigination required")]
        
        public string? Desigiation { get; set; }
        [Required(ErrorMessage = "Password required")]
        
        public string? Password { get; set; }
    }
}

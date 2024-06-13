using System.ComponentModel.DataAnnotations;

namespace EmployeeRegistrationAPI.Models
{
    public class login
    {
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage = "username required")]

        public string? Email { get; set; }
        [Required(ErrorMessage = "Password required")]
        [DataType(DataType.Password)]

        public string? Password { get; set; }
    }
}

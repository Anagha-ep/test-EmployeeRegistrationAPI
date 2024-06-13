using System.ComponentModel.DataAnnotations;

namespace EmployeeRegistrationAPI.Models
{
    public class ForgotPassword
    {

        [Key]
        public int id { get; set; }
        [Required(ErrorMessage = "Email required")]
  
        public string? Email { get; set; }

        //public bool Emailsent { get; set; }
    }
}

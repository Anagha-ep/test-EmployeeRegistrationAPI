using System.ComponentModel.DataAnnotations;

namespace EmployeeRegistrationAPI.Models
{
    public class ResetPassword
    {
        //public int id { get; set; }
        //[Required]
        [EmailAddress]

        public string Email { get; set; }



        public string Password { get; set; }

        //[Display(Name = "Confirm Password")]
        //[Compare("Password", ErrorMessage = "Password and ConfirmPassword must match")]


        //    public string ConfirmPassword { get; set; }


        //    [Required]
        //    public string Token { get; set; }
        //}
    }
}

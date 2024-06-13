using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using EmployeeRegistrationAPI.Data;
using EmployeeRegistrationAPI.Models;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Security.Policy;
using Mysqlx;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Reflection.Metadata.Ecma335;
using System.Net.Mail;
using System.Net;
using Microsoft.Win32;

namespace EmployeeRegistrationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public EmployeeController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost("register")]

        public async Task<IActionResult> Register([FromBody] Register register)
        {
            if (register != null)
            {
                bool emailExists = dbContext.Register.Any(x => x.Email == register.Email);
                if (!emailExists)
                {
                    await dbContext.Register.AddAsync(register);
                    await dbContext.SaveChangesAsync();
                    return Ok(new { Message = "User Registered" });
                }
                else
                {
                    return Ok(new { Message = "User Already Registered use a different Email !" });

                }


            }
            else
            {
                return BadRequest();
            }

        }

    
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] login loginObj)
        {
            if (loginObj == null)

                return BadRequest();
            var emp = await dbContext.Register.FirstOrDefaultAsync(x => x.Email == loginObj.Email && x.Password == loginObj.Password);
            if (emp == null)
                return NotFound(new { Message = "User name/password invalid" });
            return Ok(new { Message = "Login Success!" });
        }


        [HttpPost("forgotpassword")]

        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPassword forgotPassword)
        {

            var user = await dbContext.Register.FirstOrDefaultAsync(y => y.Email == forgotPassword.Email);
            if (user != null)
            {
                
                var token = GenerateResetToken();
                //var resetLink = Url.Action("ResetPassword", "Employee", new { Email = forgotPassword.Email, token = token }, Request.Scheme);
                var resetLink = $"http://localhost:4200/resetpassword?Email={forgotPassword.Email}&token={token}";
                await SendResetEmail(forgotPassword.Email, resetLink);
                return Ok(new { Message = "we have sent a mail" });



            }

            else
            {
                ModelState.AddModelError(nameof(forgotPassword.Email), "This email id is not registered");
            
            }


            return Ok();
        }

        [HttpPost("forgotpassword")]
        private async Task SendResetEmail(string email, string resetLink)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com");

            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("anuep1234@gmail.com", "vsbn kzol lxbs lsqo");
            var message = new MailMessage
            {
                From = new MailAddress("anuep1234@gmail.com"),
                Subject = "Password Reset",
                Body = $"Click the following link to reset your password: {resetLink}"
            };
            message.To.Add(email);
            await smtpClient.SendMailAsync(message);
        }

        private string GenerateResetToken()
        {
            var token = Guid.NewGuid().ToString();



            dbContext.SaveChanges();

            return token;
        }
        //[HttpPut]
        //public IActionResult ResetPassword(string token, string Email)
        //{
        //    if (token == null && Email == null)
        //    {
        //        return BadRequest(new { Message = "invalid reset token" }); 
        //    }
        //    return Ok();
        //}
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPassword resetPassword)
        {
            if (ModelState.IsValid)
            {

                var user1 = await dbContext.Register.FirstOrDefaultAsync(a => a.Email == resetPassword.Email);

                if (user1 != null)
                {
                    user1.Password = resetPassword.Password;
                    await dbContext.SaveChangesAsync();
                }

            }

            else
            {

                return BadRequest(new { Message = "invalid should match both" });

            }
            return Ok(new { Message = "password has bee resetted" });
        }
        
    }
}












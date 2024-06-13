using EmployeeRegistrationAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeRegistrationAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Register> Register{ get; set; }
    }
}

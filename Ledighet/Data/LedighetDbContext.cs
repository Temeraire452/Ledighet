using Ledighet.Models;
using Microsoft.EntityFrameworkCore;

namespace Ledighet.Data
{
    public class LedighetDbContext : DbContext
    {
        public LedighetDbContext(DbContextOptions<LedighetDbContext> options) : base(options)
        {

        }
        public DbSet <Employee> Employees { get; set; }
        public DbSet<LeaveApplication> LeaveApplications { get; set; }
        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<LeaveList> LeaveLists { get; set; }
    }
}

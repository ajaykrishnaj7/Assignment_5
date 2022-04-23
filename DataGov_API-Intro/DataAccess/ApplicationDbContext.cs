using Microsoft.EntityFrameworkCore;
using DataGov_API_Intro.Models;

namespace DataGov_API_Intro.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : 
            base(options) { }

        public DbSet<Parks> Parks { get; set; }
        public DbSet<Models.Park> Park { get; set; }

        public DbSet<Models.DNCComplaints> DNCComplaints { get; set; }
    }
}
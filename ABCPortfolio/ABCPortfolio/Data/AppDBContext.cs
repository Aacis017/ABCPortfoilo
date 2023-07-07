using ABCPortfolio.Models;
using Microsoft.EntityFrameworkCore;

namespace ABCPortfolio.Data
{
    public class AppDBContext :DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) :base(options)
        {   
               
        }
        public DbSet<Job>Jobs { get; set; }
        public DbSet<Requirement>Requirements { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<ContactUsViewModel> ContactUsTable { get; set; }

        public DbSet<CompletedProject> completedProjects { get; set; }
    
        public DbSet<Projects> Projects { get; set; }
     
        public DbSet<Service> Services { get; set; }
    }
}

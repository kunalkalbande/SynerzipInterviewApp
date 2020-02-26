using Microsoft.EntityFrameworkCore;

namespace SynerzipInterviewApp.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions opts) : base(opts)
        {
        }

        public DbSet<Interview> Interviews { get; set; }
        public DbSet<ContentBlock> ContentBlocks { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder Builder)
        {
            base.OnModelCreating(Builder);
        }

      
    }
}

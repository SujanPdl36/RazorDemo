using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RazorPageDemo.Model;

namespace RazorPageDemo.Data
{
    public class RDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public RDbContext(DbContextOptions options) : base(options) { }

        

        public DbSet<Login> logins { get; set; }
        public DbSet<Register> registers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Login>()
                .HasNoKey();

            modelBuilder.Entity<Register>()
                .HasKey(r => r.Id);

            // Rest of your entity configurations...

            base.OnModelCreating(modelBuilder);
        }
       
    }
}

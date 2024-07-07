using Blue.Challenge.Domain.Entities;
using Blue.Challenge.Infra.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Blue.Challenge.Infra.Context
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new ContactEntityTypeConfiguration()); 
            builder.ApplyConfiguration(new UserEntityTypeConfiguration()); 
        }
    }
}

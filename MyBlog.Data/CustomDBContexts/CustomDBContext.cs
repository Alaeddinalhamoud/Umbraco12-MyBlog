using Microsoft.EntityFrameworkCore;
using MyBlog.Data.EntityConfigurations;
using MyBlog.Domain.Models.CustomEntities.ContactUs;

namespace MyBlog.Data.CustomDBContexts
{
    public class CustomDBContext : DbContext
    {
        public CustomDBContext(DbContextOptions<CustomDBContext> options) : base(options) { }

        public required DbSet<ContactUs> ContactUs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new ContactUsEntityTypeConfiguration().Configure(modelBuilder.Entity<ContactUs>());
        }
    }
}

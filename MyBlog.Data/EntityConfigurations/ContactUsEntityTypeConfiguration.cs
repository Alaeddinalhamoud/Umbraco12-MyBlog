using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBlog.Domain.Models.CustomEntities.ContactUs;

namespace MyBlog.Data.EntityConfigurations
{
    public class ContactUsEntityTypeConfiguration : IEntityTypeConfiguration<ContactUs>
    {
        public void Configure(EntityTypeBuilder<ContactUs> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FullName).HasMaxLength(256);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(256);
            builder.Property(x => x.Subject).IsRequired().HasMaxLength(512);
            builder.Property(x => x.Message).IsRequired();

            builder.ToTable("ContactUs");
        }
    }
}

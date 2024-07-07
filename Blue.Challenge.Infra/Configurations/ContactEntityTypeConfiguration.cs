using Blue.Challenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blue.Challenge.Infra.Configurations
{
    public class ContactEntityTypeConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.ToTable("Contact");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("Id");

            builder.Property(x => x.Name)
                .HasColumnName("Name")
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Email)
                .HasColumnName("Email")
                .HasMaxLength(50);

            builder.Property(x => x.Phone)
                .HasColumnName("Phone")
                .HasMaxLength(11);

            builder.Property(x => x.UserId)
                .HasColumnName("UserId");            
        }
    }
}

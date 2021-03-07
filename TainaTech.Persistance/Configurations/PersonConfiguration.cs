using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TainaTech.Domain.Entities;

namespace TainaTech.Persistance.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.Property(p => p.PersonId)
                .HasColumnType("bigint");

            builder.Property(p => p.Firstname)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(p => p.Surname)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("varbinary");

            builder.Property(p => p.Gender)
                .IsRequired();

            builder.Property(p => p.EmailAddress)
                .IsRequired()
                .HasMaxLength(254);

            builder.Property(p => p.PhoneNumber)                
                .HasMaxLength(50);

            builder.Property(p => p.DateOfBirth)
                .IsRequired();

            builder.Property(p => p.CreatedBy)
                .HasMaxLength(100);
            builder.Property(p => p.LastModifiedBy)
                .HasMaxLength(100);
        }
    }
}

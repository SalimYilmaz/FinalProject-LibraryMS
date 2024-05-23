using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employees").HasKey(e => e.Id);

        builder.Property(e => e.Id).HasColumnName("Id").IsRequired();
        builder.Property(e => e.Name).HasColumnName("Name");
        builder.Property(e => e.Surname).HasColumnName("Surname");
        builder.Property(e => e.NationalIdentity).HasColumnName("NationalIdentity");
        builder.Property(e => e.PhoneNumber).HasColumnName("PhoneNumber");
        builder.Property(e => e.Address).HasColumnName("Address");
        builder.Property(e => e.Position).HasColumnName("Position");
        builder.Property(e => e.LibraryId).HasColumnName("LibraryId");
        builder.Property(e => e.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(e => e.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(e => e.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(e => !e.DeletedDate.HasValue);
    }
}
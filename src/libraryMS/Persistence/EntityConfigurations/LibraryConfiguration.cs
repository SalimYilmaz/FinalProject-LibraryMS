using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class LibraryConfiguration : IEntityTypeConfiguration<Library>
{
    public void Configure(EntityTypeBuilder<Library> builder)
    {
        builder.ToTable("Libraries").HasKey(l => l.Id);

        builder.Property(l => l.Id).HasColumnName("Id").IsRequired();
        builder.Property(l => l.Name).HasColumnName("Name");
        builder.Property(l => l.Address).HasColumnName("Address");
        builder.Property(l => l.PhoneNumber).HasColumnName("PhoneNumber");
        builder.Property(l => l.City).HasColumnName("City");
        builder.Property(l => l.Website).HasColumnName("Website");
        builder.Property(l => l.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(l => l.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(l => l.DeletedDate).HasColumnName("DeletedDate");

        builder.HasData(_seeds);

        builder.HasQueryFilter(l => !l.DeletedDate.HasValue);
    }

    private IEnumerable<Library> _seeds
    {
        get
        {
            yield return new()
            {
                Id = Guid.NewGuid(),
                Address = "Ýstanbul Sarýyer",
                City = "Ýstanbul",
                CreatedDate = DateTime.Now,
                Name = "Ninova Kütüphanesi",
                Website = "localhost:4200"
            };
        }
    }
}
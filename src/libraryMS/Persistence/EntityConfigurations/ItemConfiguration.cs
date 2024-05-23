using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.ToTable("Items").HasKey(i => i.Id);

        builder.Property(i => i.Id).HasColumnName("Id").IsRequired();
        builder.Property(i => i.Name).HasColumnName("Name");
        builder.Property(i => i.Type).HasColumnName("Type");
        builder.Property(i => i.Isbn).HasColumnName("Isbn");
        builder.Property(i => i.InStock).HasColumnName("InStock");
        builder.Property(i => i.Category).HasColumnName("Category");
        builder.Property(i => i.Genre).HasColumnName("Genre");
        builder.Property(i => i.PublicationDate).HasColumnName("PublicationDate");
        builder.Property(i => i.TotalPages).HasColumnName("TotalPages");
        builder.Property(i => i.Language).HasColumnName("Language");
        builder.Property(i => i.Description).HasColumnName("Description");
        builder.Property(i => i.PublisherId).HasColumnName("PublisherId");
        builder.Property(i => i.LocationId).HasColumnName("LocationId");
        builder.Property(i => i.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(i => i.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(i => i.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(i => !i.DeletedDate.HasValue);
    }
}
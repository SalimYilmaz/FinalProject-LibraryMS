using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class ItemAuthorConfiguration : IEntityTypeConfiguration<ItemAuthor>
{
    public void Configure(EntityTypeBuilder<ItemAuthor> builder)
    {
        builder.ToTable("ItemAuthors").HasKey(ia => ia.Id);

        builder.Property(ia => ia.Id).HasColumnName("Id").IsRequired();
        builder.Property(ia => ia.ItemId).HasColumnName("ItemId");
        builder.Property(ia => ia.AuthorId).HasColumnName("AuthorId");
        builder.Property(ia => ia.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(ia => ia.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(ia => ia.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(ia => !ia.DeletedDate.HasValue);
    }
}
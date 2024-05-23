using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class FineConfiguration : IEntityTypeConfiguration<Fine>
{
    public void Configure(EntityTypeBuilder<Fine> builder)
    {
        builder.ToTable("Fines").HasKey(f => f.Id);

        builder.Property(f => f.Id).HasColumnName("Id").IsRequired();
        builder.Property(f => f.MemberId).HasColumnName("MemberId");
        builder.Property(f => f.BorrowId).HasColumnName("BorrowId");
        builder.Property(f => f.FineAmount).HasColumnName("FineAmount");
        builder.Property(f => f.IsPaid).HasColumnName("IsPaid");
        builder.Property(f => f.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(f => f.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(f => f.DeletedDate).HasColumnName("DeletedDate");
        

        builder.HasQueryFilter(f => !f.DeletedDate.HasValue);
    }
}
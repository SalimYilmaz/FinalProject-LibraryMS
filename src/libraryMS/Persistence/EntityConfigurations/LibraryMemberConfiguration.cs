using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations;

public class LibraryMemberConfiguration : IEntityTypeConfiguration<LibraryMember>
{
    public void Configure(EntityTypeBuilder<LibraryMember> builder)
    {
        builder.ToTable("LibraryMembers").HasKey(lm => lm.Id);

        builder.Property(lm => lm.Id).HasColumnName("Id").IsRequired();
        builder.Property(lm => lm.LibraryId).HasColumnName("LibraryId");
        builder.Property(lm => lm.MemberId).HasColumnName("MemberId");
        builder.Property(lm => lm.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(lm => lm.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(lm => lm.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(lm => !lm.DeletedDate.HasValue);
    }
}
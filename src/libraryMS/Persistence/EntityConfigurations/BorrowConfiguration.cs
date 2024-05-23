using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nest;
using System.ComponentModel;
using System.Data;
using System;
using System.Reflection.Emit;

namespace Persistence.EntityConfigurations;

public class BorrowConfiguration : IEntityTypeConfiguration<Borrow>
{
    public void Configure(EntityTypeBuilder<Borrow> builder)
    {
        builder.ToTable("Borrows").HasKey(b => b.Id);

        builder.Property(b => b.Id).HasColumnName("Id").IsRequired();
        builder.Property(b => b.MemberId).HasColumnName("MemberId");
        builder.Property(b => b.ItemId).HasColumnName("ItemId");
        builder.Property(b => b.ReturnDate).HasColumnName("ReturnDate");
        builder.Property(b => b.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(b => b.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(b => b.DeletedDate).HasColumnName("DeletedDate");


        builder.HasQueryFilter(b => !b.DeletedDate.HasValue);
    }
}
using Application.Features.Auth.Constants;
using Application.Features.OperationClaims.Constants;
using Application.Features.UserOperationClaims.Constants;
using Application.Features.Users.Constants;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NArchitecture.Core.Security.Constants;
using Application.Features.Authors.Constants;
using Application.Features.Borrows.Constants;
using Application.Features.Employees.Constants;
using Application.Features.Fines.Constants;
using Application.Features.Items.Constants;
using Application.Features.ItemAuthors.Constants;
using Application.Features.Libraries.Constants;
using Application.Features.LibraryMembers.Constants;
using Application.Features.Locations.Constants;
using Application.Features.Members.Constants;
using Application.Features.Payments.Constants;
using Application.Features.Publishers.Constants;
using Application.Features.Reports.Constants;
using Application.Features.Reservations.Constants;
using Application.Features.Reviews.Constants;
using Application.Features.LibraryMembers.Constants;
using Application.Features.Borrows.Constants;
using Application.Features.Fines.Constants;
using Application.Features.Fines.Constants;





namespace Persistence.EntityConfigurations;

public class OperationClaimConfiguration : IEntityTypeConfiguration<OperationClaim>
{
    public void Configure(EntityTypeBuilder<OperationClaim> builder)
    {
        builder.ToTable("OperationClaims").HasKey(oc => oc.Id);

        builder.Property(oc => oc.Id).HasColumnName("Id").IsRequired();
        builder.Property(oc => oc.Name).HasColumnName("Name").IsRequired();
        builder.Property(oc => oc.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(oc => oc.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(oc => oc.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(oc => !oc.DeletedDate.HasValue);

        builder.HasData(_seeds);

        builder.HasBaseType((string)null!);
    }

    public static int AdminId => 1;
    private IEnumerable<OperationClaim> _seeds
    {
        get
        {
            yield return new() { Id = AdminId, Name = GeneralOperationClaims.Admin };

            IEnumerable<OperationClaim> featureOperationClaims = getFeatureOperationClaims(AdminId);
            foreach (OperationClaim claim in featureOperationClaims)
                yield return claim;
        }
    }

#pragma warning disable S1854 // Unused assignments should be removed
    private IEnumerable<OperationClaim> getFeatureOperationClaims(int initialId)
    {
        int lastId = initialId;
        List<OperationClaim> featureOperationClaims = new();

        #region Auth
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = AuthOperationClaims.Admin },
                new() { Id = ++lastId, Name = AuthOperationClaims.Read },
                new() { Id = ++lastId, Name = AuthOperationClaims.Write },
                new() { Id = ++lastId, Name = AuthOperationClaims.RevokeToken },
            ]
        );
        #endregion

        #region OperationClaims
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Admin },
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Read },
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Write },
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Create },
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Update },
                new() { Id = ++lastId, Name = OperationClaimsOperationClaims.Delete },
            ]
        );
        #endregion

        #region UserOperationClaims
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Admin },
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Read },
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Write },
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Create },
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Update },
                new() { Id = ++lastId, Name = UserOperationClaimsOperationClaims.Delete },
            ]
        );
        #endregion

        #region Users
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = UsersOperationClaims.Admin },
                new() { Id = ++lastId, Name = UsersOperationClaims.Read },
                new() { Id = ++lastId, Name = UsersOperationClaims.Write },
                new() { Id = ++lastId, Name = UsersOperationClaims.Create },
                new() { Id = ++lastId, Name = UsersOperationClaims.Update },
                new() { Id = ++lastId, Name = UsersOperationClaims.Delete },
            ]
        );
        #endregion

        
        #region Authors
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = AuthorsOperationClaims.Admin },
                new() { Id = ++lastId, Name = AuthorsOperationClaims.Read },
                new() { Id = ++lastId, Name = AuthorsOperationClaims.Write },
                new() { Id = ++lastId, Name = AuthorsOperationClaims.Create },
                new() { Id = ++lastId, Name = AuthorsOperationClaims.Update },
                new() { Id = ++lastId, Name = AuthorsOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Borrows
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = BorrowsOperationClaims.Admin },
                new() { Id = ++lastId, Name = BorrowsOperationClaims.Read },
                new() { Id = ++lastId, Name = BorrowsOperationClaims.Write },
                new() { Id = ++lastId, Name = BorrowsOperationClaims.Create },
                new() { Id = ++lastId, Name = BorrowsOperationClaims.Update },
                new() { Id = ++lastId, Name = BorrowsOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Employees
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = EmployeesOperationClaims.Admin },
                new() { Id = ++lastId, Name = EmployeesOperationClaims.Read },
                new() { Id = ++lastId, Name = EmployeesOperationClaims.Write },
                new() { Id = ++lastId, Name = EmployeesOperationClaims.Create },
                new() { Id = ++lastId, Name = EmployeesOperationClaims.Update },
                new() { Id = ++lastId, Name = EmployeesOperationClaims.Delete },
                new() { Id = ++lastId, Name = AuthOperationClaims.GeneralMember},
                new() { Id = ++lastId, Name = AuthOperationClaims.GeneralEmployee},
            ]
        );
        #endregion
        
        
        #region Fines
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = FinesOperationClaims.Admin },
                new() { Id = ++lastId, Name = FinesOperationClaims.Read },
                new() { Id = ++lastId, Name = FinesOperationClaims.Write },
                new() { Id = ++lastId, Name = FinesOperationClaims.Create },
                new() { Id = ++lastId, Name = FinesOperationClaims.Update },
                new() { Id = ++lastId, Name = FinesOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Items
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = ItemsOperationClaims.Admin },
                new() { Id = ++lastId, Name = ItemsOperationClaims.Read },
                new() { Id = ++lastId, Name = ItemsOperationClaims.Write },
                new() { Id = ++lastId, Name = ItemsOperationClaims.Create },
                new() { Id = ++lastId, Name = ItemsOperationClaims.Update },
                new() { Id = ++lastId, Name = ItemsOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region ItemAuthors
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = ItemAuthorsOperationClaims.Admin },
                new() { Id = ++lastId, Name = ItemAuthorsOperationClaims.Read },
                new() { Id = ++lastId, Name = ItemAuthorsOperationClaims.Write },
                new() { Id = ++lastId, Name = ItemAuthorsOperationClaims.Create },
                new() { Id = ++lastId, Name = ItemAuthorsOperationClaims.Update },
                new() { Id = ++lastId, Name = ItemAuthorsOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Libraries
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = LibrariesOperationClaims.Admin },
                new() { Id = ++lastId, Name = LibrariesOperationClaims.Read },
                new() { Id = ++lastId, Name = LibrariesOperationClaims.Write },
                new() { Id = ++lastId, Name = LibrariesOperationClaims.Create },
                new() { Id = ++lastId, Name = LibrariesOperationClaims.Update },
                new() { Id = ++lastId, Name = LibrariesOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region LibraryMembers
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = LibraryMembersOperationClaims.Admin },
                new() { Id = ++lastId, Name = LibraryMembersOperationClaims.Read },
                new() { Id = ++lastId, Name = LibraryMembersOperationClaims.Write },
                new() { Id = ++lastId, Name = LibraryMembersOperationClaims.Create },
                new() { Id = ++lastId, Name = LibraryMembersOperationClaims.Update },
                new() { Id = ++lastId, Name = LibraryMembersOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Locations
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = LocationsOperationClaims.Admin },
                new() { Id = ++lastId, Name = LocationsOperationClaims.Read },
                new() { Id = ++lastId, Name = LocationsOperationClaims.Write },
                new() { Id = ++lastId, Name = LocationsOperationClaims.Create },
                new() { Id = ++lastId, Name = LocationsOperationClaims.Update },
                new() { Id = ++lastId, Name = LocationsOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Members
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = MembersOperationClaims.Admin },
                new() { Id = ++lastId, Name = MembersOperationClaims.Read },
                new() { Id = ++lastId, Name = MembersOperationClaims.Write },
                new() { Id = ++lastId, Name = MembersOperationClaims.Create },
                new() { Id = ++lastId, Name = MembersOperationClaims.Update },
                new() { Id = ++lastId, Name = MembersOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Payments
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = PaymentsOperationClaims.Admin },
                new() { Id = ++lastId, Name = PaymentsOperationClaims.Read },
                new() { Id = ++lastId, Name = PaymentsOperationClaims.Write },
                new() { Id = ++lastId, Name = PaymentsOperationClaims.Create },
                new() { Id = ++lastId, Name = PaymentsOperationClaims.Update },
                new() { Id = ++lastId, Name = PaymentsOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Publishers
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = PublishersOperationClaims.Admin },
                new() { Id = ++lastId, Name = PublishersOperationClaims.Read },
                new() { Id = ++lastId, Name = PublishersOperationClaims.Write },
                new() { Id = ++lastId, Name = PublishersOperationClaims.Create },
                new() { Id = ++lastId, Name = PublishersOperationClaims.Update },
                new() { Id = ++lastId, Name = PublishersOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Reports
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = ReportsOperationClaims.Admin },
                new() { Id = ++lastId, Name = ReportsOperationClaims.Read },
                new() { Id = ++lastId, Name = ReportsOperationClaims.Write },
                new() { Id = ++lastId, Name = ReportsOperationClaims.Create },
                new() { Id = ++lastId, Name = ReportsOperationClaims.Update },
                new() { Id = ++lastId, Name = ReportsOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Reservations
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = ReservationsOperationClaims.Admin },
                new() { Id = ++lastId, Name = ReservationsOperationClaims.Read },
                new() { Id = ++lastId, Name = ReservationsOperationClaims.Write },
                new() { Id = ++lastId, Name = ReservationsOperationClaims.Create },
                new() { Id = ++lastId, Name = ReservationsOperationClaims.Update },
                new() { Id = ++lastId, Name = ReservationsOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Reviews
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = ReviewsOperationClaims.Admin },
                new() { Id = ++lastId, Name = ReviewsOperationClaims.Read },
                new() { Id = ++lastId, Name = ReviewsOperationClaims.Write },
                new() { Id = ++lastId, Name = ReviewsOperationClaims.Create },
                new() { Id = ++lastId, Name = ReviewsOperationClaims.Update },
                new() { Id = ++lastId, Name = ReviewsOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region LibraryMembers
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = LibraryMembersOperationClaims.Admin },
                new() { Id = ++lastId, Name = LibraryMembersOperationClaims.Read },
                new() { Id = ++lastId, Name = LibraryMembersOperationClaims.Write },
                new() { Id = ++lastId, Name = LibraryMembersOperationClaims.Create },
                new() { Id = ++lastId, Name = LibraryMembersOperationClaims.Update },
                new() { Id = ++lastId, Name = LibraryMembersOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Borrows
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = BorrowsOperationClaims.Admin },
                new() { Id = ++lastId, Name = BorrowsOperationClaims.Read },
                new() { Id = ++lastId, Name = BorrowsOperationClaims.Write },
                new() { Id = ++lastId, Name = BorrowsOperationClaims.Create },
                new() { Id = ++lastId, Name = BorrowsOperationClaims.Update },
                new() { Id = ++lastId, Name = BorrowsOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Fines
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = FinesOperationClaims.Admin },
                new() { Id = ++lastId, Name = FinesOperationClaims.Read },
                new() { Id = ++lastId, Name = FinesOperationClaims.Write },
                new() { Id = ++lastId, Name = FinesOperationClaims.Create },
                new() { Id = ++lastId, Name = FinesOperationClaims.Update },
                new() { Id = ++lastId, Name = FinesOperationClaims.Delete },
            ]
        );
        #endregion
        
        
        #region Fines
        featureOperationClaims.AddRange(
            [
                new() { Id = ++lastId, Name = FinesOperationClaims.Admin },
                new() { Id = ++lastId, Name = FinesOperationClaims.Read },
                new() { Id = ++lastId, Name = FinesOperationClaims.Write },
                new() { Id = ++lastId, Name = FinesOperationClaims.Create },
                new() { Id = ++lastId, Name = FinesOperationClaims.Update },
                new() { Id = ++lastId, Name = FinesOperationClaims.Delete },
            ]
        );
        #endregion
        
        
    
        
        return featureOperationClaims;
    }
#pragma warning restore S1854 // Unused assignments should be removed
}

using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Member : Entity<Guid>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public string NationalIdentity { get; set; }
    public Guid UserId { get; set; }
   

    public double? TotalDebt { get; set; } = 0;
    public virtual User? User { get; set; }
    public virtual ICollection<LibraryMember>? LibraryMembers { get; set; }
    public virtual ICollection<Payment>? Payments { get; set; }
    public virtual ICollection<Borrow>? Borrows { get; set; }
    public virtual ICollection<Fine>? Fines { get; set; }
    public virtual ICollection<Review>? Reviews { get; set; }
    public virtual ICollection<Report>? Reports { get; set; }
    public virtual ICollection<Reservation>? Reservations { get; set; }


    public Member()
    {
        Name = string.Empty;
        Surname = string.Empty;
        PhoneNumber = string.Empty;
        Address = string.Empty;
        UserId = Guid.Empty;

    }
}
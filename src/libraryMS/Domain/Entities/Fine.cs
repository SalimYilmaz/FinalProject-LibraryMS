using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Fine : Entity<Guid> 
{
    public Guid? MemberId { get; set; }
    public virtual Member? Member { get; set; }
    public Guid BorrowId { get; set; }  
    public virtual Borrow? Borrow { get; set; }

    public double FineAmount { get; set; }
    public bool IsPaid { get; set; }

    public Fine()
    {
        BorrowId = default(Guid);
        FineAmount = default(double);
        IsPaid = false;
    }
}
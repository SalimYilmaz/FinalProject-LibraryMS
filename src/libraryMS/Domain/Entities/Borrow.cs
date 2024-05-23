using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Borrow : Entity<Guid> 
{
    public Guid MemberId { get; set; }
    public virtual Member? Member { get; set; }
    public Guid ItemId { get; set; }
    public virtual Item? Item { get; set; }
    public DateTime ReturnDate { get; set; }
    
    
    public virtual ICollection<Fine>? Fines { get; set; }

    public Borrow()
    {
        MemberId = default(Guid);
        ItemId = default(Guid);
    }
} 
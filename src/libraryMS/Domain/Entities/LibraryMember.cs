using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class LibraryMember : Entity<Guid>
{
    public Guid? LibraryId { get; set; }
    public virtual Library? Library { get; set; }
    public Guid MemberId { get; set; }
    public virtual Member? Member { get; set; }

    public LibraryMember()
    {
        MemberId = Guid.Empty;

    }
}
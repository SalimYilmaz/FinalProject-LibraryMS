using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Reservation : Entity<Guid>
{
    public Guid MemberId { get; set; }
    public virtual Member? Member { get; set; }
    public Guid ItemId { get; set; }
    public virtual Item? Item { get; set; }

    public DateTime ReservationDate { get; set; }

    public Reservation()
    {
        MemberId = Guid.Empty;
        ItemId = Guid.Empty;
    }
}
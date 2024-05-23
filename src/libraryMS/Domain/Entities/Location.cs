using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Location : Entity<Guid>
{
    public string Section { get; set; }
    public string FloorNumber { get; set; }
    public string ShelfNumber { get; set; }

    public virtual ICollection<Item>? Items { get; set; }

    public Location()
    {
        Section = string.Empty;
        FloorNumber = string.Empty;
        ShelfNumber = string.Empty;
    }
}
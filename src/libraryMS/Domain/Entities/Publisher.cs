using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Publisher : Entity<Guid>
{
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    
    public virtual ICollection<Item>? Items { get; set; }

    public Publisher()
    {
        Name = string.Empty;
        PhoneNumber = string.Empty;
    }
}
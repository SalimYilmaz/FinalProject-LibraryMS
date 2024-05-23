using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Library : Entity<Guid>
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string City { get; set; }
    public string Website { get; set; }

    public virtual ICollection<Employee>? Employees { get; set; }    
    public virtual ICollection<LibraryMember>? LibraryMembers { get; set; }

    public virtual ICollection<Item> Items{ get; set; }

    public Library()
    {
        Name = string.Empty;
        Address = string.Empty;
        PhoneNumber = string.Empty;
        City = string.Empty;
        Website = string.Empty;
    }
}
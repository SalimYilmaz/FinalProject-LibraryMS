using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Author : Entity<Guid>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public virtual ICollection<ItemAuthor>? ItemAuthors { get; set; }

    public Author(string name, string surname)
    {
        Name = name;
        Surname = surname;
    }

    public Author()
    {
        Name = string.Empty;
        Surname = string.Empty;
    }
}
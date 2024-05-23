using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Item : Entity<Guid>
{
    public string Name { get; set; }
    public string Type { get; set; }
    public string Isbn { get; set; }  
    public string Category { get; set; }
    public string Genre { get; set; }
    public DateTime PublicationDate { get; set; } 
    public short TotalPages { get; set; }  
    public string Language { get; set; }
    public string Description { get; set; }
    public int InStock { get; set; }
    
    public virtual ICollection<ItemAuthor>? ItemAuthors { get; set; }
    public virtual Publisher? Publisher { get; set; }
    public Guid PublisherId { get; set; }
    public virtual Library? Library { get; set; }
    public Guid LibraryId { get; set; }
    public virtual Location? Location { get; set; }
    public Guid LocationId { get; set; }
    public virtual ICollection<Borrow>? Borrows { get; set; }
    public virtual ICollection<Review>? Reviews { get; set; }
    public virtual ICollection<Reservation>? Reservations { get; set; }

    public Item()
    {
        Name = string.Empty;
        Type = string.Empty;
        Category = string.Empty;
        Genre = string.Empty;
        TotalPages = default(int);
        Language = string.Empty;
        Description = string.Empty;
    }
}
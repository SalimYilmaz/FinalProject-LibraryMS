using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class ItemAuthor : Entity<Guid>
{
    public Guid? ItemId { get; set; }
    public virtual Item? Item { get; set; }
    public Guid AuthorId { get; set; }
    public virtual Author? Author { get; set; }
     // ToDo:Join yapılacak itemauthor gette // 

    public ItemAuthor()
    {
        //AuthorId = Guid.Empty;
    }
}
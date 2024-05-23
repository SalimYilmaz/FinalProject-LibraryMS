﻿using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Review : Entity<Guid>
{
    public Guid MemberId { get; set; }
    public virtual Member? Member { get; set; }
    public Guid ItemId { get; set; }
    public virtual Item? Item { get; set; }
    public string ReviewTitle { get; set; }
    public DateTime ReviewDate { get; set; }
    public string ReviewContent { get; set; }

    public Review()
    {
        MemberId = Guid.Empty;
        ItemId = Guid.Empty;
        ReviewTitle = string.Empty;
        ReviewContent = string.Empty;
    }
}
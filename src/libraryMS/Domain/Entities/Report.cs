using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Report : Entity<Guid>
{
    public Guid MemberId { get; set; }
    public virtual Member? Member { get; set; }

    public string ReportTitle { get; set; }
    public DateTime ReportDate { get; set; }
    public string ReportContent { get; set; }   
    public string Status { get; set; }

    public Report()
    {
        MemberId = default(Guid);
        ReportTitle = string.Empty;
        ReportContent = string.Empty;
        Status = string.Empty;
    }
}
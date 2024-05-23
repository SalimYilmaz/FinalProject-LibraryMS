using NArchitecture.Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Locations.Queries.GetDynamic;
public class GetDynamicLocationItemDto : IDto
{
    public string Section { get; set; }
    public string FloorNumber { get; set; }
    public string ShelfNumber { get; set; }

}

using NArchitecture.Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Authors.Queries.GetDynamic;
public class GetDynamicAuthorItemDto : IDto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public Guid ItemId {  get; set; }
}


using NArchitecture.Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Items.Queries.GetDynamic;
public  class GetDynamicItemDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string? ISBN { get; set; }
    public string? ISSN { get; set; }
    public string Category { get; set; }
    public string Genre { get; set; }
    public DateTime PublicationDate { get; set; }
    public short TotalPages { get; set; }
    public string Language { get; set; }
    public string? Description { get; set; }
    public string PublisherName { get; set; }
    public string LibraryName{ get; set; } 
    public string Section { get; set; }
    public string FloorNumber {  get; set; }
    public string ShelfNumber { get; set; }
}

using NArchitecture.Core.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ItemAuthors.Queries.GetDynamic;
public class GetDynamicItemAuthorDto : IResponse
{
    public Guid Id { get; set; }
    public string AuthorName { get; set; }
    public string AuthorSurname { get; set; }
    public string ItemName { get; set; }
    public string ItemType { get; set; }
    public string? ItemISBN { get; set; }
    public string? ItemISSN { get; set; }
    public string ItemCategory { get; set; }
    public string ItemGenre { get; set; }
    public DateTime ItemPublicationDate { get; set; }
    public short ItemTotalPages { get; set; }
    public string ItemLanguage { get; set; }
    public string ItemDescription { get; set; }
    public int ItemInStock { get; set; }
}

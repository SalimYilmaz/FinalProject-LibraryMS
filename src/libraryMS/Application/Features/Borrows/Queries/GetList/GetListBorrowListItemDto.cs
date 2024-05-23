using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Borrows.Queries.GetList;

public class GetListBorrowListItemDto : IDto
{
    public string ItemName { get; set; }
    public Guid Id { get; set; }
}
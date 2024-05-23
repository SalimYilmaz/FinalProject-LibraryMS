using Application.Features.LibraryMembers.Commands.Create;
using Application.Features.LibraryMembers.Commands.Delete;
using Application.Features.LibraryMembers.Commands.Update;
using Application.Features.LibraryMembers.Queries.GetById;
using Application.Features.LibraryMembers.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LibraryMembersController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateLibraryMemberCommand createLibraryMemberCommand)
    {
        CreatedLibraryMemberResponse response = await Mediator.Send(createLibraryMemberCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateLibraryMemberCommand updateLibraryMemberCommand)
    {
        UpdatedLibraryMemberResponse response = await Mediator.Send(updateLibraryMemberCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedLibraryMemberResponse response = await Mediator.Send(new DeleteLibraryMemberCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdLibraryMemberResponse response = await Mediator.Send(new GetByIdLibraryMemberQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListLibraryMemberQuery getListLibraryMemberQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListLibraryMemberListItemDto> response = await Mediator.Send(getListLibraryMemberQuery);
        return Ok(response);
    }
}
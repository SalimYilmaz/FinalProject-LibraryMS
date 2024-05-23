using Application.Features.Members.Commands.Create;
using Application.Features.Members.Commands.Delete;
using Application.Features.Members.Commands.Update;
using Application.Features.Members.Queries.GetById;
using Application.Features.Members.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;
using Application.Services.ItemAuthors;
using Domain.Entities;
using Application.Services.Repositories;
using Application.Services.LibraryMembers;
using NArchitecture.Core.Persistence.Dynamic;
using Application.Features.Items.Queries.GetDynamic;
using Application.Features.Members.Queries.GetDynamic;
using Application.Services.UsersService;
using Application.Features.Libraries.Commands.Create;
using Application.Features.LibraryMembers.Commands.Create;
using Application.Features.Users.Queries.GetById;
using Application.Features.Users.Commands.Delete;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MembersController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateMemberCommand createMemberCommand)
    {
        CreatedMemberResponse response = await Mediator.Send(createMemberCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateMemberCommand updateMemberCommand)
    {
        UpdatedMemberResponse response = await Mediator.Send(updateMemberCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedMemberResponse response = await Mediator.Send(new DeleteMemberCommand { Id = id });

        await Mediator.Send(new DeleteUserCommand { Id = getUserIdFromRequest() });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdMemberResponse response = await Mediator.Send(new GetByIdMemberQuery {Id = id});
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListMemberQuery getListMemberQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListMemberItemDto> response = await Mediator.Send(getListMemberQuery);
        return Ok(response);
    }
    [HttpPost("dynamic")]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest, [FromBody] DynamicQuery dynamic)
    {
        GetDynamicMemberQuery getDynamicMemberQuery = new() { PageRequest = pageRequest, Dynamic = dynamic };
        GetListResponse<GetDynamicMemberItemDto> response = await Mediator.Send(getDynamicMemberQuery);
        return Ok(response);
    }
}
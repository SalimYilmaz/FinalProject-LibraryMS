using Application.Features.Borrows.Commands.Create;
using Application.Features.Borrows.Commands.Delete;
using Application.Features.Borrows.Commands.Update;
using Application.Features.Borrows.Queries.GetById;
using Application.Features.Borrows.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;
using Persistence.Repositories;
using NArchitecture.Core.Persistence.Repositories;
using AutoMapper;
using Application.Services.Repositories;
using Domain.Entities;
using Application.Features.Auth.Dtos;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BorrowsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateBorrowCommand createBorrowCommand)
    {
        CreatedBorrowResponse response = await Mediator.Send(createBorrowCommand);
        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateBorrowCommand updateBorrowCommand)
    {
        UpdatedBorrowResponse response = await Mediator.Send(updateBorrowCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedBorrowResponse response = await Mediator.Send(new DeleteBorrowCommand {Id = id});
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdBorrowResponse response = await Mediator.Send(new GetByIdBorrowQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest, [FromQuery] bool isUserInProfile)
    {
        GetListBorrowQuery getListBorrowQuery = new() { PageRequest = pageRequest ,CurrentUserRoles = getUserRolesFromRequest(), IsUserInProfile = isUserInProfile ,UserId = getUserIdFromRequest()};
        GetListResponse<GetListBorrowListItemDto> response = await Mediator.Send(getListBorrowQuery);
        return Ok(response);
    }
}
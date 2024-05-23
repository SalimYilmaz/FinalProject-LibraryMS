using Application.Features.Fines.Commands.Create;
using Application.Features.Fines.Commands.Delete;
using Application.Features.Fines.Commands.Update;
using Application.Features.Fines.Queries.GetById;
using Application.Features.Fines.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FinesController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateFineCommand createFineCommand)
    {
        CreatedFineResponse response = await Mediator.Send(createFineCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateFineCommand updateFineCommand)
    {
        UpdatedFineResponse response = await Mediator.Send(updateFineCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedFineResponse response = await Mediator.Send(new DeleteFineCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdFineResponse response = await Mediator.Send(new GetByIdFineQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListFineQuery getListFineQuery = new() { PageRequest = pageRequest ,UserId = getUserIdFromRequest(), CurrentUserRoles = getUserRolesFromRequest()};
        GetListResponse<GetListFineListItemDto> response = await Mediator.Send(getListFineQuery);
        return Ok(response);
    }
}
using Application.Features.Reservations.Commands.Create;
using Application.Features.Reservations.Commands.Delete;
using Application.Features.Reservations.Commands.Update;
using Application.Features.Reservations.Queries.GetById;
using Application.Features.Reservations.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReservationsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateReservationCommand createReservationCommand)
    {
        CreatedReservationResponse response = await Mediator.Send(createReservationCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateReservationCommand updateReservationCommand)
    {
        UpdatedReservationResponse response = await Mediator.Send(updateReservationCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedReservationResponse response = await Mediator.Send(new DeleteReservationCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdReservationResponse response = await Mediator.Send(new GetByIdReservationQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListReservationQuery getListReservationQuery = new() { PageRequest = pageRequest ,UserId = getUserIdFromRequest(), CurrentUserRoles = getUserRolesFromRequest()};
        GetListResponse<GetListReservationListItemDto> response = await Mediator.Send(getListReservationQuery);
        return Ok(response);
    }
}
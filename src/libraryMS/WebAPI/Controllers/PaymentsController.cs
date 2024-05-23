using Application.Features.Payments.Commands.Create;
using Application.Features.Payments.Commands.Delete;
using Application.Features.Payments.Commands.Update;
using Application.Features.Payments.Queries.GetById;
using Application.Features.Payments.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreatePaymentCommand createPaymentCommand)
    {
        createPaymentCommand.IpAddress = getIpAddress();
        CreatedPaymentResponse response = await Mediator.Send(createPaymentCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdatePaymentCommand updatePaymentCommand)
    {
        UpdatedPaymentResponse response = await Mediator.Send(updatePaymentCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedPaymentResponse response = await Mediator.Send(new DeletePaymentCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdPaymentResponse response = await Mediator.Send(new GetByIdPaymentQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListPaymentQuery getListPaymentQuery = new() { PageRequest = pageRequest,UserId = getUserIdFromRequest(),CurrentUserRoles = getUserRolesFromRequest() };
        GetListResponse<GetListPaymentListItemDto> response = await Mediator.Send(getListPaymentQuery);
        return Ok(response);
    }
}
using Application.Features.Reviews.Commands.Create;
using Application.Features.Reviews.Commands.Delete;
using Application.Features.Reviews.Commands.Update;
using Application.Features.Reviews.Queries.GetById;
using Application.Features.Reviews.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReviewsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateReviewCommand createReviewCommand)
    {
        CreatedReviewResponse response = await Mediator.Send(createReviewCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateReviewCommand updateReviewCommand)
    {
        UpdatedReviewResponse response = await Mediator.Send(updateReviewCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedReviewResponse response = await Mediator.Send(new DeleteReviewCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdReviewResponse response = await Mediator.Send(new GetByIdReviewQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListReviewQuery getListReviewQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListReviewListItemDto> response = await Mediator.Send(getListReviewQuery);
        return Ok(response);
    }
}
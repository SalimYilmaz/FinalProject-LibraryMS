using Application.Features.ItemAuthors.Commands.Create;
using Application.Features.ItemAuthors.Commands.Delete;
using Application.Features.ItemAuthors.Commands.Update;
using Application.Features.ItemAuthors.Queries.GetById;
using Application.Features.ItemAuthors.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;
using NArchitecture.Core.Persistence.Dynamic;
using Application.Features.ItemAuthors.Queries.GetDynamic;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ItemAuthorsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateItemAuthorCommand createItemAuthorCommand)
    {
        CreatedItemAuthorResponse response = await Mediator.Send(createItemAuthorCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateItemAuthorCommand updateItemAuthorCommand)
    {
        UpdatedItemAuthorResponse response = await Mediator.Send(updateItemAuthorCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedItemAuthorResponse response = await Mediator.Send(new DeleteItemAuthorCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdItemAuthorResponse response = await Mediator.Send(new GetByIdItemAuthorQuery { Id = id });

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListItemAuthorQuery getListItemAuthorQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListItemAuthorListItemDto> response = await Mediator.Send(getListItemAuthorQuery);
        return Ok(response);
    }

    [HttpPost("dynamic")]
    public async Task<IActionResult> GetListDynamic([FromQuery] PageRequest pageRequest,[FromBody] DynamicQuery dynamic)
    {
        GetDynamicItemAuthorQuery getDynamicItemAuthorQuery = new() { PageRequest = pageRequest,Dynamic = dynamic };
        GetListResponse<GetDynamicItemAuthorDto> response = await Mediator.Send(getDynamicItemAuthorQuery);
        return Ok(response);
    }
}
using Application.Features.Items.Commands.Create;
using Application.Features.Items.Commands.Delete;
using Application.Features.Items.Commands.Update;
using Application.Features.Items.Queries.GetById;
using Application.Features.Items.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;
using NArchitecture.Core.Persistence.Dynamic;
using Application.Features.Items.Queries.GetDynamic;
using Application.Features.ItemAuthors.Commands.Create;
using Application.Services.MailService;
using MailKit;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ItemsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateItemCommand createItemCommand)
    {
        CreatedItemResponse response = await Mediator.Send(createItemCommand);
        return Created(uri: "", response);
    }
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateItemCommand updateItemCommand)
    {
        UpdatedItemResponse response = await Mediator.Send(updateItemCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedItemResponse response = await Mediator.Send(new DeleteItemCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdItemResponse response = await Mediator.Send(new GetByIdItemQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListItemQuery getListItemQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListItemListItemDto> response = await Mediator.Send(getListItemQuery);
        return Ok(response);
    }

    [HttpPost("dynamic")]

    public async Task<IActionResult> GetListDynamic([FromQuery] PageRequest pageRequest, [FromBody] DynamicQuery dynamic)
    {
        GetDynamicItemQuery query = new GetDynamicItemQuery() { Dynamic = dynamic , PageRequest = pageRequest};
        var response = await Mediator.Send(query); 
        return Ok(response);
    }
}
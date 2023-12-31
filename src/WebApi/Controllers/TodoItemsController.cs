﻿using Microsoft.AspNetCore.Mvc;
using ProjectName.ServiceName.Application.Common.Models;
using ProjectName.ServiceName.Application.TodoItems.Commands.CreateTodoItem;
using ProjectName.ServiceName.Application.TodoItems.Commands.DeleteTodoItem;
using ProjectName.ServiceName.Application.TodoItems.Commands.UpdateTodoItem;
using ProjectName.ServiceName.Application.TodoItems.Commands.UpdateTodoItemDetail;
using ProjectName.ServiceName.Application.TodoItems.Queries.GetTodoItemsWithPagination;

namespace ProjectName.ServiceName.WebApi.Controllers;


public class TodoItemsController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PaginatedList<TodoItemBriefDto>>> GetTodoItemsWithPagination(
        [FromQuery] GetTodoItemsWithPaginationQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPost]
    [ApiVersion("1.0")]
    [Route("create")]
    public async Task<ActionResult<int>> CreateV1(CreateTodoItemCommand command)
    {
        return await Mediator.Send(command);
    }
    
    [HttpPost]
    [ApiVersion("2.0")]
    [Route("create")]
    public async Task<ActionResult<int>> Create(CreateTodoItemCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Update(int id, UpdateTodoItemCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpPut("[action]")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> UpdateItemDetails(int id, UpdateTodoItemDetailCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteTodoItemCommand(id));

        return NoContent();
    }
}

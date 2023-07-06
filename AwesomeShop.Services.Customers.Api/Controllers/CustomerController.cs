using AwesomeShop.Services.Customers.Application.Commands.Customers.AddCommand;
using AwesomeShop.Services.Customers.Application.Commands.Customers.UpdateCommand;
using AwesomeShop.Services.Customers.Application.Queries.Customers.GetByIdCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeShop.Services.Customers.Api.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomersController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddCustomerCommandInputModel command)
    {
        var id = await _mediator.Send(command);

        return Created($"api/customers/{id}", value: null);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var query = new GetCustomerByIdQueryInputModel(id);

        var customerViewModel = await _mediator.Send(query);

        if (customerViewModel == null)
        {
            return NotFound();
        }

        return Ok(customerViewModel);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, [FromBody] UpdateCustomerCommandInputModel command)
    {
        command.Id = id;

        await _mediator.Send(command);

        return NoContent();
    }
}
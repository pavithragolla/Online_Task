using Microsoft.AspNetCore.Mvc;
using online.DTOs;
using online.Models;
using online.Repositories;
using Online.Repositories;

namespace online.Controllers;

[ApiController]
[Route("api/customer")]

public class CustomerController : ControllerBase
{
    private readonly ILogger<CustomerController> _logger;
    private readonly ICustomerRepository _customer;
    private readonly IOrderRepository _order;
    private readonly IProductRepository _product;

    public CustomerController(ILogger<CustomerController> logger, ICustomerRepository customer,  IOrderRepository order, IProductRepository product)
    {
        _logger = logger;
        _customer = customer;
        _product = product;
        _order = order;
    }

    [HttpPost]
    public async Task<ActionResult<CustomerDTO>> CreateCustomer([FromBody] CustomerCreateDTO Data)
    {
        var ToCreateCustomer = new Customer
        {
            Name = Data.Name,
            Address = Data.Address,
            Mobile = Data.Mobile,
            Email = Data.Email
        };
        var CreatedCustomer = await _customer.Create(ToCreateCustomer);

        return StatusCode(StatusCodes.Status201Created, CreatedCustomer.asDto);
    }

    [HttpGet]
    public async Task<ActionResult<List<CustomerDTO>>> GetAllUser()
    {
        var usersList = await _customer.GetList();


        var dtoList = usersList.Select(x => x.asDto);

        return Ok(dtoList);
    }

    [HttpGet("{customer_id}")]

    public async Task<ActionResult<CustomerDTO>> GetUserById([FromRoute] int customer_id)
    {

        var Customer = await _customer.GetById(customer_id);

        if (Customer is null)
            return NotFound("No Customer found with given Customer_id");


        var dto = Customer.asDto;
        // dto.Students = (await _student.GetAllForClass(id)).Select(x => x.asDto).ToList();

        dto.Product = (await _product.GetAllForCustomer(customer_id)).Select(x => x.asDto).ToList();
        dto.Order = (await _order.GetAllForCustomer(customer_id)).Select(x => x.asDto).ToList();
        return Ok(dto);
    }

    [HttpPut("{customer_id}")]

    public async Task<ActionResult> UpdateCustomer([FromRoute] int customer_id,
    [FromBody] CustomerUpdateDTO Data)
    {
        var existing = await _customer.GetById(customer_id);
        if (existing is null)
            return NotFound("No Customer found with given Customer Id");

        var toUpdateCustomer = existing with
        {
            Address = Data.Address,
            Mobile = Data.Mobile,
            Email = Data.Email

        };

        var didUpdate = await _customer.Update(toUpdateCustomer);

        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not update Customer");

        return NoContent();
    }
}

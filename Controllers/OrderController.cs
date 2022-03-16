using Microsoft.AspNetCore.Mvc;
using online.DTOs;
using online.Models;
using online.Repositories;
using Online.DTOs;
using Online.Models;
using Online.Repositories;

namespace online.Controllers;

[ApiController]
[Route("api/order")]

public class OrderController : ControllerBase
{
    private readonly ILogger<OrderController> _logger;
    private readonly IOrderRepository _Order;
    private readonly IProductRepository _product;

    public OrderController(ILogger<OrderController> logger, IOrderRepository Order, IProductRepository product)
    {
        _logger = logger;
        _Order = Order;
        _product = product;
    }

    [HttpPost]
    public async Task<ActionResult<OrderDTO>> CreateOrder([FromBody] OrderCreateDTO Data)
    {
        var ToCreateOrder = new Order
        {
            OrderDate = Data.OrderDate,
            DeliveryDate =Data.DeliveryDate,
            CustomerId =Data.CustomerId
        };
        var CreatedOrder = await _Order.Create(ToCreateOrder);

        return StatusCode(StatusCodes.Status201Created, CreatedOrder.asDto);
    }

    [HttpGet]
    public async Task<ActionResult<List<OrderDTO>>> GetAllUser()
    {
        var usersList = await _Order.GetList();


        var dtoList = usersList.Select(x => x.asDto);

        return Ok(dtoList);
    }

    [HttpGet("{Order_id}")]

    public async Task<ActionResult<OrderDTO>> GetUserById([FromRoute] int Order_id)
    {

        var Order = await _Order.GetById(Order_id);

        if (Order is null)
            return NotFound("No Order found with given Order_id");


        var dto = Order.asDto;
        dto.Product = (await _product.GetAllForOrder(Order_id)).Select(x => x.asDto).ToList();
        return Ok(dto);
    }

    [HttpPut("{Order_id}")]

    public async Task<ActionResult> UpdateOrder([FromRoute] int Order_id,
    [FromBody] OrderUpdateDTO Data)
    {
        var existing = await _Order.GetById(Order_id);
        if (existing is null)
            return NotFound("No Order found with given Order Id");

        var toUpdateOrder = existing with
        {
          DeliveryDate =Data.DeliveryDate 

        };

        var didUpdate = await _Order.Update(toUpdateOrder);

        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not update Order");

        return NoContent();
    }
}

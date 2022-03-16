using Microsoft.AspNetCore.Mvc;
using online.Models;
using Online.DTOs;
using Online.Repositories;

namespace Online.Controllers;

[ApiController]
[Route("api/product")]

public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly IProductRepository _product;
    private readonly ITagRepository _tag;


    public ProductController(ILogger<ProductController> logger, IProductRepository product, ITagRepository tag)
    {
        _logger = logger;
        _product = product;
        _tag = tag;
    }
    [HttpPost]
    public async Task<ActionResult<ProductDTO>> CreateProduct([FromBody] ProductCreateDTO Data)
    {
        var ToCreateProduct = new Product
        {
            Name = Data.Name,
            Price = Data.Price,
            OrderId = Data.OrderId
        };
        var CreatedProduct = await _product.Create(ToCreateProduct);

        return StatusCode(StatusCodes.Status201Created, CreatedProduct.asDto);
    }
    [HttpGet]
    public async Task<ActionResult<List<ProductDTO>>> GetAllUser()
    {
        var usersList = await _product.GetList();


        var dtoList = usersList.Select(x => x.asDto);

        return Ok(dtoList);
    }

     [HttpGet("{id}")]

    public async Task<ActionResult<ProductDTO>> GetUserById([FromRoute] int id)
    {

        var Product = await _product.GetById(id);

        if (Product is null)
            return NotFound("No Product found with given Product_id");


        var dto = Product.asDto;
        dto.Tag = (await _tag.GetAllForProduct(Product.Id)).Select(x => x.asDto).ToList();
        return Ok(dto);
    }
      [HttpPut("{product_id}")]

    public async Task<ActionResult> UpdateProduct([FromRoute] int Product_id,
    [FromBody] ProductUpdateDTO Data)
    {
        var existing = await _product.GetById(Product_id);
        if (existing is null)
            return NotFound("No Product found with given Product Id");

        var toUpdateProduct = existing with
        {
            Price = Data.Price,

        };

        var didUpdate = await _product.Update(toUpdateProduct);

        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not update Product");

        return NoContent();
    }
    
}
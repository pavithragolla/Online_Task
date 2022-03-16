using Online.DTOs;

namespace online.Models;

public record Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int OrderId { get; set; }

    public ProductDTO asDto => new ProductDTO
    {
        Name = Name,
        Price = Price,
        OrderId = OrderId
    };
}
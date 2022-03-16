using System.Text.Json.Serialization;

namespace Online.DTOs;

public record OrderDTO
{
     [JsonPropertyName("orderdate")]
    public DateTimeOffset OrderDate { get; set; }

     [JsonPropertyName("deliverydate")]
    public DateTimeOffset DeliveryDate { get; set; }

     [JsonPropertyName("customerid")]
    public int CustomerId { get; set; }

    public List<ProductDTO> Product { get; set; }
}

public record OrderCreateDTO
{
     [JsonPropertyName("orderdate")]
    public DateTimeOffset OrderDate { get; set; }

     [JsonPropertyName("deliverydate")]
    public DateTimeOffset DeliveryDate { get; set; }

     [JsonPropertyName("customerid")]
    public int CustomerId { get; set; }
}
public record OrderUpdateDTO
{

     [JsonPropertyName("deliverydate")]
    public DateTimeOffset DeliveryDate { get; set; }
}
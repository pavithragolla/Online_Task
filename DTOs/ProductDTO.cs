using System.Text.Json.Serialization;

namespace Online.DTOs;

public record ProductDTO
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("price")]
    public decimal Price { get; set; }
    [JsonPropertyName("order_id")]
    public int OrderId { get; set; }
    
      public List<TagDTO> Tag { get; set; }
}

public record ProductCreateDTO
{
    
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("price")]
    public decimal Price { get; set; }
    [JsonPropertyName("order_id")]
    public int OrderId { get; set; }

  

}
public record ProductUpdateDTO
{
    [JsonPropertyName("price")]
    public decimal Price { get; set; }
}
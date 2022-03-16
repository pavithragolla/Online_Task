using System.Text.Json.Serialization;
using Online.DTOs;

namespace online.DTOs;

public record CustomerDTO
{
    // [JsonPropertyName("id")]
    // public int Id { get; set; }
    [JsonPropertyName("name")]
     public string Name { get; set; }
    [JsonPropertyName("address")]
    public  string Address { get; set; }
    [JsonPropertyName("mobile")]
    public long Mobile { get; set; }
    [JsonPropertyName("email")]
    public string Email { get; set; }

 public List<OrderDTO> Order {get;set; }
 public List<ProductDTO> Product {get; set; }

}

public record CustomerCreateDTO
{
    [JsonPropertyName("name")]
     public string Name { get; set; }

    [JsonPropertyName("address")]
    public  string Address { get; set; }

    [JsonPropertyName("mobile")]
    public long Mobile { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }
}

public record CustomerUpdateDTO
{
    [JsonPropertyName("address")]
    public  string Address {get; set; }

    [JsonPropertyName("mobile")]
    public long Mobile { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }
}


using Online.DTOs;

namespace Online.Models;

public record Tag
{
    public string Name { get; set; }
    public int ProductId { get; set; }

    public TagDTO asDto => new TagDTO
    {
        Name = Name,
        ProductId = ProductId
    };
}
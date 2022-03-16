namespace Online.DTOs;

public record TagDTO
{
    public string Name { get; set; }
    public int ProductId { get; set; }
    
}
public record TagCreateDTO
{
    public string Name { get; set; }
    public int ProductId { get; set; }
}
using online.DTOs;

namespace online. Models;

public record Customer
{
    public string Name { get; set; }
    public  string Address{get; set; }
    public long Mobile { get; set; }
    public string Email { get; set; }

    public CustomerDTO asDto => new CustomerDTO
    {
       Name = Name,
       Address = Address,
       Mobile = Mobile,
       Email = Email  
    };
}
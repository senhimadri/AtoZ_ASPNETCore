using System.ComponentModel.DataAnnotations;

namespace MinimalAPI.Models;

public class Customer
{
    [Key]
    public Guid CustomerId { get; init; }
    public string UserName { get; init; } = default!;
    public string FullName { get; init; } = default!;
    public string Email { get; init; } = default!;
    public DateTime DateofBirth { get; set; }
}

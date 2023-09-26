namespace FluentValidationPros.Models;

public class CustomerOrder
{
    public List<OrderList> Orders { get; set; }= new List<OrderList>();
}

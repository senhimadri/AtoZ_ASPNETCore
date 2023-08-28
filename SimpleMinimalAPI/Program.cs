using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<CustomerRepository>();


var app = builder.Build();

app.MapGet("/customers", ([FromServices] CustomerRepository repo)=>
{
    var customers = repo.GetAllCustomer();
    return  customers.Count() !=0 ? Results.Ok(customers): Results.NotFound();
});

app.MapGet("/customers/{id}", ([FromServices] CustomerRepository repo, Guid Id)=>
{
    var customer= repo.GetCustomerbyId(Id);
    return customer is not null ? Results.Ok(customer): Results.NotFound();
});

app.MapPost("/customers", ([FromServices] CustomerRepository repo, Customer customer) =>
{
    repo.CreateCustomer(customer);
    return Results.Created($"/customers/{customer.Id}", customer);
});

app.MapPut("/customers", ([FromServices] CustomerRepository repo, Customer customer) =>
{
    var prevustomer = repo.GetCustomerbyId(customer.Id);
    if (prevustomer is null)
    {
        return Results.NotFound();
    }
    repo.UpdateCustomer(customer);
    return Results.Ok(customer);
});

app.MapDelete("/customers/{id}", ([FromServices] CustomerRepository repo, Guid Id) =>
{
    repo.DeleteCustomer(Id);
    return Results.Ok();
});

app.Run();

record Customer(Guid Id,string FullName);


class CustomerRepository
{
    private readonly Dictionary<Guid, Customer> _customers = new();

    public void CreateCustomer(Customer customer)
    {
        if (customer is null)
        {
            return;
        }
        _customers[customer.Id] = customer;
    }

    public Customer GetCustomerbyId(Guid Id)
    {
        return _customers[Id];
    }

    public List<Customer> GetAllCustomer()
    {
        return _customers.Values.ToList();
    }

    public void UpdateCustomer(Customer customer)
    {
        var existingCustomer = _customers[customer.Id];

        if (existingCustomer is null)
        {
            return;
        }

        _customers[customer.Id] = customer;
    }

    public void DeleteCustomer(Guid Id)
    {
        _customers.Remove(Id);
    }
}

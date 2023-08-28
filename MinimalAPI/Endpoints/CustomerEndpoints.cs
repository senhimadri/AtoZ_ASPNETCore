using Microsoft.EntityFrameworkCore.ChangeTracking;
using MinimalAPI.Data;
using MinimalAPI.Models;
using MinimalAPI.Services;

namespace MinimalAPI.Endpoints
{
    public static class CustomerEndpoints
    {
        public static void  MapCustomerEndpoints(this WebApplication app)
        {
            app.MapPost("customers", CreateCustomer);
        }

        public static async Task<IResult> CreateCustomer(Customer customer,ICustomerService customerService)
        {
            var cus = await customerService.CreateCustomerAsync(customer);
            return Results.Created($"/customers/{cus.MyProperty}", cus);
        }
    }
}

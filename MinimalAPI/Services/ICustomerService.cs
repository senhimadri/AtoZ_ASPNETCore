using Microsoft.AspNetCore.Mvc;
using MinimalAPI.Models;

namespace MinimalAPI.Services
{
    public interface ICustomerService
    {
        public Task<List<Customer>> GetAllCustomerAsync();
        public Task<Customer> CreateCustomerAsync(Customer customer);
        public Task<Customer> UpdateCustomerAsync(Customer customer);
        public Task<Customer> GetCustomerByIdAsync(Guid customerId);
        public Task<Customer> DeleteCustomerAsync(Guid customerId);
    }
}

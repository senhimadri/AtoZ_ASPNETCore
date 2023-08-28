using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalAPI.Data;
using MinimalAPI.Models;

namespace MinimalAPI.Services
{
    public class CustomerService: ICustomerService
    {
        protected readonly SQLLightDBContext _dbContext;
        public CustomerService(SQLLightDBContext dbContext) => _dbContext = dbContext;

        public async Task<List<Customer>> GetAllCustomerAsync()
        {
            var Customers =await _dbContext.CustomerInformation.ToListAsync();
            return Customers ?? new List<Customer>();
        }
        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            await _dbContext.CustomerInformation.AddAsync(customer);
            await _dbContext.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer> DeleteCustomerAsync(Guid customerId)
        {
            var customer = await _dbContext.CustomerInformation.Where(x=>x.MyProperty== customerId).FirstOrDefaultAsync();

            if (customer is null)
            {
                return new Customer();
            }  
            else
            {
                _dbContext.Remove(customer);
                await _dbContext.SaveChangesAsync();

                return customer;
            }
        }

        public async Task<Customer> GetCustomerByIdAsync(Guid customerId)
        {
            var customer =await  _dbContext.CustomerInformation.Where(x => x.MyProperty == customerId).FirstOrDefaultAsync()?? new Customer();
            return customer ?? new Customer();  
        }

        public async Task<Customer> UpdateCustomerAsync(Customer customer)
        {
            _dbContext.CustomerInformation.Update(customer);
            await _dbContext.SaveChangesAsync();

            return customer;
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RedisImplementation.Data;
using RedisImplementation.Models;
using RedisImplementation.Services;

namespace RedisImplementation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DriverController : ControllerBase
{
    private readonly ILogger<DriverController> _logger;
    private readonly ICacheService _cashServer;
    private readonly AppDBContext _context;

    public DriverController(ILogger<DriverController> logger, ICacheService cashServer, AppDBContext context) 
                => (_logger, _cashServer, _context) = (logger, cashServer, context);

    [HttpGet("drivers")]
    public async Task<IActionResult> Get()
    {
        var cashData = _cashServer.GetData<IEnumerable<Driver>>("drivers");

        if (cashData != null && cashData.Count() > 0)
            return Ok(cashData);

        cashData = await _context.Drivers.ToListAsync();

        var expiryTime = DateTimeOffset.Now.AddSeconds(30);

        _cashServer.SetData<IEnumerable<Driver>>("drivers", cashData, expiryTime);

        return Ok(cashData);
    }

    [HttpPost("AddDriver")]
    public async Task<IActionResult> Post (Driver value)
    {
        var addedObj = await _context.Drivers.AddAsync(value);

        var expiryTime = DateTimeOffset.Now.AddSeconds(30);

        _cashServer.SetData<Driver>("driver", addedObj.Entity, expiryTime);

        await _context.SaveChangesAsync();

        return Ok(addedObj.Entity);
    }

    [HttpDelete("DeleteDriver")]
    public async Task<IActionResult> Delete(int id)
    {
        var exit = await _context.Drivers.FirstOrDefaultAsync(x=>x.Id == id);

        if (exit != null)
        {
            _context.Remove(exit);
            _cashServer.RemoveData($"driver{id}");
            await _context.SaveChangesAsync();

            return NoContent();
        }

        return NotFound();  
    }
}

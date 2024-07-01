using Microsoft.AspNetCore.Mvc;
using RedisImplementation.Services;

namespace RedisImplementation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DriverController : ControllerBase
{
    private readonly ILogger<DriverController> _logger;
    private readonly ICacheService _cashServer;

    public DriverController(ILogger<DriverController> logger, ICacheService cashServer) 
                => (_logger, _cashServer) = (logger, cashServer);


}

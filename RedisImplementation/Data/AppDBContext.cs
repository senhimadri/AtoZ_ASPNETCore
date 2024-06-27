using Microsoft.EntityFrameworkCore;
using RedisImplementation.Models;

namespace RedisImplementation.Data;

public class AppDBContext : DbContext
{
	public AppDBContext(DbContextOptions<AppDBContext> options): base (options)
	{

	}
	public DbSet<Driver> Drivers { get; set; }
}

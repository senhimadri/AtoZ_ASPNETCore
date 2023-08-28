using Microsoft.EntityFrameworkCore;
using MinimalAPI.Models;
using System.Collections.Generic;

namespace MinimalAPI.Data;

public class SQLLightDBContext : DbContext
{
    public SQLLightDBContext()
    {

    }
    public SQLLightDBContext(DbContextOptions<SQLLightDBContext> options)
            : base(options)
    {
    }
    public DbSet<Customer> CustomerInformation { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseSqlite(@"Data Source =SQLLightDB\SQLLightBD.db");


}

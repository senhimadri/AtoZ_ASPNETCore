using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCoreDocumentation.Models;

public class Blog
{
    public int BlogId { get; set; }
    public string Url { get; set; } = string.Empty;
    public List<Post> Posts { get; } = new();
}


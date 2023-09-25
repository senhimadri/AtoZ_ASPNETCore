using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleMinimalAPI;
using System.Text.Json;
using Microsoft.AspNetCore.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddSingleton<CustomerRepository>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//==================================Configure JSON serialization options globally ======================================
//builder.Services.ConfigureHttpJsonOptions(options=>
//{
//    options.SerializerOptions.WriteIndented = true;
//    options.SerializerOptions.IncludeFields = true;
//});
//======================================================================================================================


var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

// ====================================== Endpoint Extension =======================================
app.AddUserEndpoints();
//===========================================***********============================================



//========================================= Configure JSON serialization options for an endpoint =======================
var options = new JsonSerializerOptions(JsonSerializerDefaults.Web)
{
    WriteIndented = true
};
app.MapGet("/serializationTest", ()=> Results.Json(new Todo { Name="JSON Serilizer Test.",IsComplete=false}, options )).WithOpenApi();

app.MapGet("/serializationTest2", (HttpContext context) =>
    context.Response.WriteAsJsonAsync<Todo>(
        new Todo { Name = "Walk dog", IsComplete = false }, options));
//=================================================*******************==================================================


//========================================== Use the TypedResults API ===================================================
var todItemGroup = app.MapGroup("/todoitems/TypedResults");

todItemGroup.MapGet("/",GetAllTodos);
todItemGroup.MapGet("/complete", GetCompletedTodos);
todItemGroup.MapGet("/{id}", GetTodobyid);
todItemGroup.MapPost("/", CreateTodo);
todItemGroup.MapPut("/{id}", UpdateTodo);
todItemGroup.MapDelete("/{id}", DeleteTodo);


static async Task<IResult> GetAllTodos(TodoDb db)
{
    return TypedResults.Ok(await db.Todos.ToListAsync());
}

static async Task<IResult> GetCompletedTodos(TodoDb db)
{
    return TypedResults.Ok(await db.Todos.Where(t => t.IsComplete).ToListAsync());
}
static async Task<IResult> GetTodobyid(int id, TodoDb db)
{
    return await db.Todos.FindAsync(id) is Todo todo ? TypedResults.Ok(todo): TypedResults.NotFound();
}

static async Task<IResult> CreateTodo(Todo todo,TodoDb db)
{
    db.Add(todo);
    await db.SaveChangesAsync();
    return TypedResults.Ok(todo);
}

static async Task<IResult> UpdateTodo(int id, Todo inputTodo, TodoDb db)
{
    var todo = await db.Todos.FindAsync(id);

    if (todo is null) return TypedResults.NotFound();

    todo.Name = inputTodo.Name;
    todo.IsComplete = inputTodo.IsComplete;

    await db.SaveChangesAsync();

    return TypedResults.NoContent();
}

static async Task<IResult> DeleteTodo(int id, TodoDb db)
{
    if (await db.Todos.FindAsync(id) is Todo todo)
    {
        db.Todos.Remove(todo);
        await db.SaveChangesAsync();
        return TypedResults.NoContent();
    }

    return TypedResults.NotFound();
}
//============================================*********************=======================================================

//========================================== Use the MapGroup API=========================================================
var todoitemgroup = app.MapGroup("/todoitemsgroup");

todoitemgroup.MapGet("/", async (TodoDb db) =>
    await db.Todos.ToListAsync());

todoitemgroup.MapGet("/complete", async (TodoDb db) =>
    await db.Todos.Where(t => t.IsComplete).ToListAsync());

todoitemgroup.MapGet("/{id}", async (int id, TodoDb db) =>
    await db.Todos.FindAsync(id) is Todo todo ? Results.Ok(todo) : Results.NotFound());

todoitemgroup.MapPost("/", async (Todo todo, TodoDb db) =>
{
    db.Todos.Add(todo);
    await db.SaveChangesAsync();
    return Results.Created($"/todoitemsgroup/{todo.Id}", todo);
});

todoitemgroup.MapPut("/{id}", async (int id, Todo inputTodo, TodoDb db) =>
{
    var todo = await db.Todos.FindAsync(id);

    if (todo is null) return Results.NotFound();

    todo.Name = inputTodo.Name;
    todo.IsComplete = inputTodo.IsComplete;

    db.Todos.Update(todo);
    db.SaveChanges();

    return Results.NoContent();
});

todoitemgroup.MapDelete("/{id}", async (int id, TodoDb db) =>
{
    if (await db.Todos.FindAsync(id) is Todo todo)
    {
        db.Todos.Remove(todo);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }
    return Results.NotFound();
});


//===============================================************=============================================================



//===================================================== From DBContext ====================================================

app.MapGet("/todoitems",async (TodoDb db) => 
    await db.Todos.ToListAsync());

app.MapGet("/todoitems/complete", async (TodoDb db) =>
    await db.Todos.Where(t=>t.IsComplete).ToListAsync());

app.MapGet("/todoitems/{id}", async (int id, TodoDb db) => 
    await db.Todos.FindAsync(id) is Todo todo ? Results.Ok(todo):Results.NotFound());


app.MapPost("/todoitems", async (Todo todo, TodoDb db)=>
{
    db.Todos.Add(todo);
    await db.SaveChangesAsync();
    return Results.Created($"/todoitems/{todo.Id}",todo);
});

app.MapPut("/todoitems/{id}", async (int id,Todo inputTodo,TodoDb db)=>
{
    var todo = await db.Todos.FindAsync(id);

    if (todo is null) return Results.NotFound();

    todo.Name = inputTodo.Name;
    todo.IsComplete=inputTodo.IsComplete;

    db.Todos.Update(todo);
    db.SaveChanges();

    return Results.NoContent();
});

app.MapDelete("/todoitems/{id}", async (int id, TodoDb db)=>
{
    if (await db.Todos.FindAsync(id) is Todo todo)
    {
        db.Todos.Remove(todo);
        await db.SaveChangesAsync();
        return Results.NoContent() ;
    }
    return Results.NotFound();
});

//==================================================== ********************** ====================================================


app.MapGet("/",()=> "Hello World.");

app.MapGet("/users/{userId}/books/{booksId}", (int userId,int bookId)=> $"The UserId is {userId} and the BookId is {bookId }");

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

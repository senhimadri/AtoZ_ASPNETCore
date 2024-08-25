using DependencyInjectionCheck.Services;
using Microsoft.Extensions.DependencyInjection;


var serviceProvider = new ServiceCollection()
                .AddScoped<IServices, Service>()   // Scoped
                .AddTransient<IServices, Service>() // Transient
                .AddSingleton<IServices, Service>() // Singleton
                .BuildServiceProvider();

// Demonstrate AddScoped
Console.WriteLine("Scoped:");
using (var scope1 = serviceProvider.CreateScope())
{
    var service1 = scope1.ServiceProvider.GetService<IServices>();
    var service2 = scope1.ServiceProvider.GetService<IServices>();
    Console.WriteLine($"Service1 Operation ID: {service1!.GetOperationId()}");
    Console.WriteLine($"Service2 Operation ID: {service2!.GetOperationId()}");
}

using (var scope2 = serviceProvider.CreateScope())
{
    var service3 = scope2.ServiceProvider.GetService<IServices>();
    Console.WriteLine($"Service3 Operation ID: {service3!.GetOperationId()}");
}

Console.WriteLine();

// Demonstrate AddTransient
Console.WriteLine("Transient:");
var transientService1 = serviceProvider.GetService<IServices>();
var transientService2 = serviceProvider.GetService<IServices>();
Console.WriteLine($"Transient Service1 Operation ID: {transientService1!.GetOperationId()}");
Console.WriteLine($"Transient Service2 Operation ID: {transientService2!.GetOperationId()}");

Console.WriteLine();

// Demonstrate AddSingleton
Console.WriteLine("Singleton:");
var singletonService1 = serviceProvider.GetService<IServices>();
var singletonService2 = serviceProvider.GetService<IServices>();
Console.WriteLine($"Singleton Service1 Operation ID: {singletonService1.GetOperationId()}");
Console.WriteLine($"Singleton Service2 Operation ID: {singletonService2.GetOperationId()}");

Console.ReadLine();

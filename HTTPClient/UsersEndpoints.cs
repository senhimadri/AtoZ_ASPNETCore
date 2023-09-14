﻿using Microsoft.AspNetCore.Mvc;

namespace HTTPClient;

public static class UsersEndpoints
{
    public static IEndpointRouteBuilder MapUserEndpoints(this WebApplication app)
    {
        app.MapGet("/postapiget", ([FromServices] HTTPClientExample _rx) =>
        {
            var AS = _rx.httpExample();
            return Results.Ok(AS);
        });

        return app;
    }
}


public class HTTPClientExample
{
    static readonly HttpClient httpClient = new HttpClient();

    public  async Task httpExample()
    {
        try
        {
            using HttpResponseMessage response = await httpClient.GetAsync("https://jsonplaceholder.typicode.com/posts");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }
}

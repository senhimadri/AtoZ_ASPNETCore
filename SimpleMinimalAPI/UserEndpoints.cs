namespace SimpleMinimalAPI;

public static class UserEndpoints
{
    public static void AddUserEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/EndpointExtension", () => Results.Json(new Todo { Name = "JSON Serilizer Test.", IsComplete = false }));
    }
}

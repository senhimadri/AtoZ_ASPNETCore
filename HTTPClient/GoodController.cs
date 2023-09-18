namespace HTTPClient;

public class GoodController
{
    private static readonly HttpClient _httpClient;

    static GoodController()
    {
        var socketsHandler = new SocketsHttpHandler()
        {
            PooledConnectionIdleTimeout=TimeSpan.FromMinutes(5),        
        };

        _httpClient = new HttpClient(socketsHandler);
    }

}

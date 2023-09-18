namespace HTTPClient;

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

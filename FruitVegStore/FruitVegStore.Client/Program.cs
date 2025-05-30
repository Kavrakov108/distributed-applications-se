using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

class Program
{
    static async Task Main(string[] args)
    {
        var client = new HttpClient();
        var loginData = new { Username = "admin", Password = "password" };
        var content = new StringContent(JsonSerializer.Serialize(loginData), Encoding.UTF8, "application/json");

        var response = await client.PostAsync("https://localhost:5001/api/auth/login", content);
        var token = await response.Content.ReadAsStringAsync();

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var productsResponse = await client.GetAsync("https://localhost:5001/api/products");
        var products = await productsResponse.Content.ReadAsStringAsync();

        Console.WriteLine(products);
    }
}

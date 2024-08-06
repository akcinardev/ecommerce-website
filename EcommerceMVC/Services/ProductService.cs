using EcommerceMVC.Models;
using System.Text.Json;

namespace EcommerceMVC.Services
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public ProductService(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _httpClient = httpClientFactory.CreateClient("Api");
            _config = config;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            var response = await _httpClient.GetStringAsync("/products");
            Console.WriteLine(response);
            return JsonSerializer.Deserialize<IEnumerable<Product>>(response)!;
        }
    }
}

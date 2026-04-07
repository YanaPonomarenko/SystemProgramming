using OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OnlineStore.Services;

public class StoreApiService
{
    private readonly HttpClient _httpClient;

    public StoreApiService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<Product[]> GetAllProductsAsync()
    {
        string url = "https://fakestoreapi.com/products";
        HttpResponseMessage response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        string json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<Product[]>(json);
    }
    public async Task<Product> GetProductByIdAsync(int id)
    {
        string url = $"https://fakestoreapi.com/products/{id}";
        HttpResponseMessage response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        string json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<Product>(json);
    }
}

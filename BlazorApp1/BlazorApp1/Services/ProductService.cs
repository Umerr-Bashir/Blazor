using BlazorApp1.DTOs;
using BlazorApp1.Models;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace BlazorApp1.Services
{
    public class ProductService
    {
        private readonly HttpClient _http;

        public ProductService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<ProductResponseDTO>?> GetAllProducts()
        {
            var response = await _http.GetFromJsonAsync<ApiResponse<List<ProductResponseDTO>>>($"api/product/list");
            return response?.Data;
        }

    }
}

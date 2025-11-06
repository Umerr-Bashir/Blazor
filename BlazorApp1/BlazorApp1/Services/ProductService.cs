using BlazorApp1.DTOs;
using BlazorApp1.Models;
using ECommerceApp.DTOs.CategoryDTOs;
using ECommerceApp.DTOs.ProductDTOs;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System.Runtime.InteropServices;

namespace BlazorApp1.Services
{
    public class ProductService
    {
        private readonly HttpClient _http;

        public ProductService(HttpClient http)
        {
            _http = http;
        }

        public async Task<ApiResponse<ProductResponseDTO>> AddProductAsync(ProductCreateDTO product)
        {
            var response = await _http.PostAsJsonAsync($"api/Products/CreateProduct", product);
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<ProductResponseDTO>>();

            if (result == null)
            {
                return new ApiResponse<ProductResponseDTO>(
                    500,
                    errors: new List<string> { "No response from server." }
                );
            }
            if (!result.Success)
            {
                return new ApiResponse<ProductResponseDTO>(
                    500,
                    errors: new List<string> { "No response from server." }
                );
            }
            return result;
        }
        public async Task<ApiResponse<List<ProductResponseDTO>>> GetAllProducts()
        {
            var response = await _http.GetAsync($"api/product/list");
            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return new ApiResponse<List<ProductResponseDTO>>(404, errors: new List<string> { "Product not found for this user." });
                }
                return new ApiResponse<List<ProductResponseDTO>>((int)response.StatusCode, errors: new List<string> { "Request failed." });
            }
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<List<ProductResponseDTO>>>();
            return result ?? new ApiResponse<List<ProductResponseDTO>>(500, errors: new List<string> { "Invalid server response." });
        }

    }
}

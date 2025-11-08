using BlazorApp1.DTOs;
using BlazorApp1.Models;
using EcommerceApp.DTOs;
using ECommerceApp.DTOs.CategoryDTOs;
using ECommerceApp.DTOs.ProductDTOs;
using Microsoft.AspNetCore.Components.Forms;
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

        public async Task<ApiResponse<ProductResponseDTO>> AddProductAsync(ProductCreateDTO product, IBrowserFile file)
        {
            try { 
            var content = new MultipartFormDataContent();

            // Adding Normal fields
            content.Add(new StringContent(product.Name), "Name");
            content.Add(new StringContent(product.Description), "Description");
            content.Add(new StringContent(product.Price.ToString()), "Price");
            content.Add(new StringContent(product.StockQuantity.ToString()), "StockQuantity");
            content.Add(new StringContent(product.DiscountPercentage.ToString()), "DiscountPercentage");
            content.Add(new StringContent(product.CategoryId.ToString()), "CategoryId");

            if (file != null)
            {
                var stream = file.OpenReadStream(5_000_000);
                var fileContent = new StreamContent(stream);

                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
                content.Add(fileContent, "ImageUrl", file.Name);
            }

            var response = await _http.PostAsync($"api/Products/CreateProduct", content);
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<ProductResponseDTO>>(new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (result == null)
            {
                return new ApiResponse<ProductResponseDTO>(
                    500,
                    errors: new List<string> { "Server returned invalid response." }
                );
            }
            if (!result.Success)
            {
                return new ApiResponse<ProductResponseDTO>(
                    500,
                    errors: result.Errors ?? new List<string> { "Unknown Server Error." }
                );
            }
            return result;

            }catch (Exception ex)
{
                return new ApiResponse<ProductResponseDTO>(
                    500,
                    errors: new List<string> { "Invalid JSON returned from server: " + ex.Message }
                );
            }
        }
        public async Task<ApiResponse<List<ProductResponseDTO>>> GetAllProducts()
        {
            var response = await _http.GetAsync($"api/Products/GetAllProducts");
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

        public async Task<ApiResponse<ConfirmationResponseDTO>> DeleteProductAsync(int Id)
        {
            var response = await _http.DeleteAsync($"api/Products/DeleteProduct/{Id}");
            if (!response.IsSuccessStatusCode) {
                return new ApiResponse<ConfirmationResponseDTO>(500, new List<string> { "Invalid Server Response" });
            }
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<ConfirmationResponseDTO>>();
            return result ?? new ApiResponse<ConfirmationResponseDTO>(500, new List<string> { "Error" }); ;
        }

        public async Task<ApiResponse<ConfirmationResponseDTO>> UpdateProductStatusAsync(ProductStatusUpdateDTO product)
        {
            var response = await _http.PutAsJsonAsync($"api/Products/UpdateProductStatus/", product);
            if (!response.IsSuccessStatusCode) {
                return new ApiResponse<ConfirmationResponseDTO>(500, new List<string> { "Invalid Server Response." });
            }
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<ConfirmationResponseDTO>>();
            return result ?? new ApiResponse<ConfirmationResponseDTO>(500, new List<string> { "Error" });
        }

    }

    
}

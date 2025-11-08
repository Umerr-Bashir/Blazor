using BlazorApp1.DTOs;
using EcommerceApp.DTOs;
using ECommerceApp.DTOs.CategoryDTOs;
using ECommerceApp.DTOs.ProductDTOs;
using ECommerceApp.DTOs.ShoppingCartDTOs;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorApp1.Services
{
    public class CartService
    {
        private readonly HttpClient _http;

        public CartService(HttpClient http)
        {
            _http = http;
        }

        public async Task<ApiResponse<CartResponseDTO>> GetCartItemAsync(int Id)
        {
            var response = await _http.GetAsync($"api/Carts/GetCart/{Id}");
            // On 404 or any
            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return new ApiResponse<CartResponseDTO>(404, errors: new List<string> { "Category not found for this user." });
                }

                return new ApiResponse<CartResponseDTO>((int)response.StatusCode, errors: new List<string> { "Request failed." });
            }

            // On success:
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<CartResponseDTO>>();
            return result ?? new ApiResponse<CartResponseDTO>(500, errors: new List<string> { "Invalid server response." });
        }
        public async Task<ApiResponse<CartResponseDTO>> AddToCartAsync(AddToCartDTO cart)
        {
            var response = await _http.PostAsJsonAsync("api/Carts/AddToCart", cart);
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<CartResponseDTO>>();

            if (result == null)
            {
                return new ApiResponse<CartResponseDTO>(
                    500,
                    errors: new List<string> { "No response from server." }
                );
            }
            if (!result.Success)
            {
                return new ApiResponse<CartResponseDTO>(
                    500,
                    errors: new List<string> { "An error occurred." }
                );
            }
            return result ?? new ApiResponse<CartResponseDTO>(500, errors: new List<string> { "No response from server." });
        }

        public async Task<ApiResponse<CartResponseDTO>> RemoveCartItemAsync(RemoveCartItemDTO removeCart)
        {
            var response = await _http.PostAsJsonAsync($"api/Carts/RemoveCartItem", removeCart);
            if (!response.IsSuccessStatusCode)
            {
                return new ApiResponse<CartResponseDTO>(500, errors: new List<string> { "Invalid server response." });
            }
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<CartResponseDTO>>();
            return result ?? new ApiResponse<CartResponseDTO>(500, errors: new List<string> { "Error" });
        }
    }
}

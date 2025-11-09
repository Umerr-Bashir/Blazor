using BlazorApp1.DTOs;
using BlazorApp1.Models;
using ECommerceApp.DTOs.AddressesDTOs;
using ECommerceApp.DTOs.CategoryDTOs;
using ECommerceApp.DTOs.OrderDTOs;
using Microsoft.AspNetCore.Http;

namespace BlazorApp1.Services
{
    public class OrderService
    {
        private readonly HttpClient _http;

        public OrderService(HttpClient http)
        {
            _http = http;
        }

        public async Task<ApiResponse<OrderResponseDTO>> CreateOrderAsync(OrderCreateDTO orderCreateDTO)
        {
            var response = await _http.PostAsJsonAsync($"api/Orders/CreateOrder", orderCreateDTO);
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<OrderResponseDTO>>();

            if (result == null)
            {
                return new ApiResponse<OrderResponseDTO>(
                    500,
                    errors: new List<string> { "No response from server." }
                );
            }
            if (!result.Success)
            {
                return new ApiResponse<OrderResponseDTO>(
                    500,
                    errors: new List<string> { "No response from server." }
                );
            }
            return result;
        }
        public async Task<List<OrderResponseDTO>?> GetAllOrders()
        {
            var response = await _http.GetFromJsonAsync<ApiResponse<List<OrderResponseDTO>>>($"api/Order/GetAll");
            return response?.Data;
        }

        public async Task<ApiResponse<OrderResponseDTO>> GetOrderById(int? Id)
        {
            var response = await _http.GetAsync ($"api/Orders/GetOrderById/{Id}");
            // On 404 or any
            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return new ApiResponse<OrderResponseDTO>(404, errors: new List<string> { "Order not found for this user." });
                }

                return new ApiResponse<OrderResponseDTO>((int)response.StatusCode, errors: new List<string> { "Request failed." });
            }

            // On success:
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<OrderResponseDTO>>();
            return result ?? new ApiResponse<OrderResponseDTO>(500, errors: new List<string> { "Invalid server response." });
        }
        public async Task<ApiResponse<List<OrderResponseDTO>>> GetOrderByCustomerId(int? Id)
        {
            var response = await _http.GetAsync($"api/Orders/GetOrdersByCustomer/{Id}");
            // On 404 or any
            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return new ApiResponse<List<OrderResponseDTO>>(404, errors: new List<string> { "Order not found for this user." });
                }

                return new ApiResponse<List<OrderResponseDTO>>((int)response.StatusCode, errors: new List<string> { "Request failed." });
            }

            // On success:
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<List<OrderResponseDTO>>>();
            return result ?? new ApiResponse<List<OrderResponseDTO>>(500, errors: new List<string> { "Invalid server response." });
        }


    }
}

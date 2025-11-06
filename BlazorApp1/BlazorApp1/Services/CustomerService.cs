using BlazorApp1.DTOs;
using EcommerceApp.DTOs.CustomerDTO;
using ECommerceApp.DTOs.CustomerDTOs;
using System.Net.Http.Json;

namespace BlazorApp1.Services
{
    public class CustomerService
    {
        private readonly HttpClient _http;
        private readonly AuthService _authService;

        public CustomerService(HttpClient http, AuthService authService)
        {
            _http = http;
            _authService = authService;
        }

        public async Task<ApiResponse<CustomerResponseDTO>> GetProfileAsync()
        {
            var userId = await _authService.GetUserIdAsync();
            if (string.IsNullOrEmpty(userId))
            {
                return new ApiResponse<CustomerResponseDTO>
                (500, errors: new List<string> { "UserId not found." });
            }
            
            var response = await _http.GetFromJsonAsync<ApiResponse<CustomerResponseDTO>>($"api/Customers/GetCustomerById/{userId}");

            return response ?? new ApiResponse<CustomerResponseDTO>
                (500,errors: new List<string> { "No response from server." });
        }

    }
}

using BlazorApp1.DTOs;
using EcommerceApp.DTOs.CustomerDTO;
using ECommerceApp.DTOs.AddressesDTOs;
using ECommerceApp.DTOs.CustomerDTOs;
using ECommerceApp.DTOs.ProductDTOs;
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

        public async Task<ApiResponse<CustomerResponseDTO>> GetCustomerById(int Id)
        {
            var response = await _http.GetAsync($"api/Customers/GetCustomerById/{Id}");
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<CustomerResponseDTO>>();

            // On 404 or any
            if (!result.Success)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return new ApiResponse<CustomerResponseDTO>(404, errors: new List<string> { result.Errors.FirstOrDefault() ?? "Address not found for this user." });
                }

                return new ApiResponse<CustomerResponseDTO>((int)response.StatusCode, errors: new List<string> { result.Errors.FirstOrDefault() ?? "Request failed." });
            }

            // On success
            return result ?? new ApiResponse<CustomerResponseDTO>(500, errors: new List<string> { result.Errors.FirstOrDefault() ?? "Invalid server response." });
        }

    }
}

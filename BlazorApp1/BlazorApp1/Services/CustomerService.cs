using BlazorApp1.Models;
using EcommerceApp.DTOs.CustomerDTO;
using ECommerceApp.DTOs.CustomerDTOs;

namespace BlazorApp1.Services
{
    public class CustomerService
    {
        private readonly HttpClient _http;

        public CustomerService(HttpClient http)
        {
            _http = http;
        }

        public async Task<ApiResponse<CustomerResponseDTO>> Register(CustomerRegistrationDTO register)
        {
            var response = await _http.PostAsJsonAsync("api/Customers/RegisterCustomer", register);

            var result = await response.Content.ReadFromJsonAsync<ApiResponse<CustomerResponseDTO>>();

            // If server didn't return a valid ApiResponse
            if (result == null)
            {
                return new ApiResponse<CustomerResponseDTO>(
                    500,
                    errors: new List<string> { "No response from server." }
                );
            }

            return result;
        }

        public async Task<ApiResponse<LoginResponseDTO>> Login(LoginDTO login)
        {
            var response = await _http.PostAsJsonAsync("api/Customers/Login", login);

            var result = await response.Content.ReadFromJsonAsync<ApiResponse<LoginResponseDTO>>();

            // If server didn't return a valid ApiResponse
            if (result == null)
            {
                return new ApiResponse<LoginResponseDTO>(
                    500,
                    errors: new List<string> { "No response from server." }
                );
            }

            return result;
        }


    }
}

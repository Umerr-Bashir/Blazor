using BlazorApp1.DTOs;
using Blazored.LocalStorage;
using EcommerceApp.DTOs.CustomerDTO;
using ECommerceApp.DTOs.CustomerDTOs;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace BlazorApp1.Services
{
    public class AuthService
    {
        private readonly HttpClient _http;
        private readonly ILocalStorageService _localStorage;

        public AuthService(HttpClient http, ILocalStorageService localStorage)
        {
            _http = http;
            _localStorage = localStorage;
        }

        public async Task<ApiResponse<CustomerResponseDTO>> Register(CustomerRegistrationDTO register)
        {
            var response = await _http.PostAsJsonAsync("api/Auth/RegisterCustomer", register);

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
            var response = await _http.PostAsJsonAsync("api/Auth/Login", login);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                return new ApiResponse<LoginResponseDTO>((int)response.StatusCode, error);
            }

            var result = await response.Content.ReadFromJsonAsync<ApiResponse<LoginResponseDTO>>();

            await _localStorage.SetItemAsync("authToken", result!.Data.Token);
            await _localStorage.SetItemAsync("userId", result!.Data.CustomerId);
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.Data.Token);

            return result ?? new ApiResponse<LoginResponseDTO>(500, "Failed to parse server response.");

        }
        public async Task<string?> GetUserIdAsync() =>
            await _localStorage.GetItemAsync<string>("userId");
    }
}

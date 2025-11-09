using BlazorApp1.DTOs;
using BlazorApp1.DTOs.ModalDTOs;
using EcommerceApp.DTOs.AddressDTO;
using ECommerceApp.DTOs.AddressesDTOs;
using ECommerceApp.DTOs.CustomerDTOs;
using ECommerceApp.DTOs.OrderDTOs;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Text.Json;

namespace BlazorApp1.Services
{
    public class AddressService
    {
        private readonly HttpClient _http;
        private readonly AuthService _authService;

        public AddressService(HttpClient httpClient, AuthService authService)
        {
            _http = httpClient;
            _authService = authService;
        }

        public async Task<ApiResponse<AddressResponseDTO>> AddAddressAsync(AddressCreateDTO address)
        {
            var userId = await _authService.GetUserIdAsync();
            if (string.IsNullOrEmpty(userId))
                return new ApiResponse<AddressResponseDTO>(500, errors: new() { "UserId not found!" });

            address.CustomerId = int.Parse(userId);

            var response = await _http.PostAsJsonAsync("api/Addresses/CreateAddress", address);

            // Read raw text ALWAYS
            var raw = await response.Content.ReadAsStringAsync();

            ApiResponse<AddressResponseDTO>? result;

            try
            {
                result = JsonSerializer.Deserialize<ApiResponse<AddressResponseDTO>>(raw,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (Exception)
            {
                return new ApiResponse<AddressResponseDTO>(500,
                    errors: new List<string> { "Server returned unexpected error format", raw });
            }

            return result ?? new ApiResponse<AddressResponseDTO>(500, errors: new() { "Null response" });
        }


        public async Task<ApiResponse<List<AddressResponseDTO>>> GetAddressAsync()
        {
            try
            {
                var userId = await _authService.GetUserIdAsync();
                if (string.IsNullOrEmpty(userId))
                {
                    return new ApiResponse<List<AddressResponseDTO>>(500, errors: new List<string> { "UserId not found!" });
                }

                var httpResponse = await _http.GetAsync($"api/Addresses/GetAddressesByCustomer/{userId}");

                // On 404 or any
                if (!httpResponse.IsSuccessStatusCode)
                {
                    if (httpResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        return new ApiResponse<List<AddressResponseDTO>>(404, errors: new List<string> { "Address not found for this user." });
                    }

                    return new ApiResponse<List<AddressResponseDTO>>((int)httpResponse.StatusCode, errors: new List<string> { "Request failed." });
                }

                // On success:
                var response = await httpResponse.Content.ReadFromJsonAsync<ApiResponse<List<AddressResponseDTO>>>();
                return response ?? new ApiResponse<List<AddressResponseDTO>>(500, errors: new List<string> { "Invalid server response." });
            }
            catch (Exception ex)
            {
                {
                    Console.WriteLine(ex.InnerException);
                    return null;
                }
            }
        }

        public async Task<ApiResponse<AddressDeleteDTO>?> DeleteAddressAsync(int Id)
        {
            var httpResponse = await _http.DeleteAsync($"api/Addresses/DeleteAddress/{Id}");
            if (!httpResponse.IsSuccessStatusCode)
            {
                return new ApiResponse<AddressDeleteDTO>(500, errors: new List<string> { "Invalid server response." });
            }
            var result = await httpResponse.Content.ReadFromJsonAsync<ApiResponse<AddressDeleteDTO>>();
            return result;
        }

        public async Task<ApiResponse<AddressResponseDTO>> GetAddressByIdAsync(int Id)
        {
            var response = await _http.GetAsync($"api/Addresses/GetAddressById/{Id}");
            // On 404 or any
            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return new ApiResponse<AddressResponseDTO>(404, errors: new List<string> { "Address not found for this user." });
                }

                return new ApiResponse<AddressResponseDTO>((int)response.StatusCode, errors: new List<string> { "Request failed." });
            }

            // On success
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<AddressResponseDTO>>();
            return result ?? new ApiResponse<AddressResponseDTO>(500, errors: new List<string> { "Invalid server response." });
        }



    }
}

using BlazorApp1.DTOs;
using EcommerceApp.DTOs;
using EcommerceApp.DTOs.AddressDTO;
using ECommerceApp.DTOs.AddressesDTOs;
using ECommerceApp.DTOs.CategoryDTOs;
using ECommerceApp.DTOs.CustomerDTOs;
using ECommerceApp.DTOs.PaymentDTOs;
using Microsoft.AspNetCore.Http;
using static BlazorApp1.Components.Modals.EditProfileModal;

namespace BlazorApp1.Services
{
    public class CategoryService
    {
        private readonly HttpClient _http;

        public CategoryService(HttpClient http)
        {
            _http = http;
        }

        public async Task<ApiResponse<CategoryResponseDTO>>AddCategoryAsync(CategoryCreateDTO category)
        {
            var response = await _http.PostAsJsonAsync("api/Categories/CreateCategory", category);
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<CategoryResponseDTO>>();

            if (result == null)
            {
                return new ApiResponse<CategoryResponseDTO>(
                    500,
                    errors: new List<string> { result?.Errors.FirstOrDefault() ?? "Unknowm errorr" }
                );
            }
            if (!result.Success)
            {
                return new ApiResponse<CategoryResponseDTO>(
                    500,
                    errors: new List<string> { result.Errors.FirstOrDefault() ?? "Unknown errorr" }
                );
            }

            return result;
        }

        public async Task<ApiResponse<List<CategoryResponseDTO>>> GetCategoriesAsync()
        {
            var response = await _http.GetAsync($"api/Categories/GetAllCategories");
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<List<CategoryResponseDTO>>>();

            // On 404 or any
            if (!result.Success)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return new ApiResponse<List<CategoryResponseDTO>>(404, errors: new List<string> { "Category not found for this user." });
                }

                return new ApiResponse<List<CategoryResponseDTO>>((int)response.StatusCode, errors: new List<string> { result.Errors.FirstOrDefault() ?? "Unknowm errorr" });
            }

            // On success:
            return result ?? new ApiResponse<List<CategoryResponseDTO>>(500, errors: new List<string> { result?.Errors.FirstOrDefault() ?? "Unknowm errorr" });
        }

        public async Task<ApiResponse<CategoryResponseDTO>> GetCategoriesById(int Id)
        {
            var response = await _http.GetAsync($"api/Categories/GetCategoryById/{Id}");
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<CategoryResponseDTO>>();

            // On 404 or any
            if (!result.Success)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return new ApiResponse<CategoryResponseDTO>(404, errors: new List<string> { result.Errors.FirstOrDefault() ?? "Category not found for this user." });
                }

                return new ApiResponse<CategoryResponseDTO>((int)response.StatusCode, errors: new List<string> { result.Errors.FirstOrDefault() ?? "Request failed." });
            }

            // On success
            return result ?? new ApiResponse<CategoryResponseDTO>(500, errors: new List<string> { result.Errors.FirstOrDefault() ?? "Invalid server response." });
        }

        public async Task<ApiResponse<ConfirmationResponseDTO>> DeleteCategoryAsync(int Id)
        {
            var response = await _http.DeleteAsync($"api/Categories/DeleteCategory/{Id}");
            if (!response.IsSuccessStatusCode) {
                return new ApiResponse<ConfirmationResponseDTO>(500, errors: new List<string> { "Invalid server response." });
            }
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<ConfirmationResponseDTO>>();
            return result ?? new ApiResponse<ConfirmationResponseDTO>(500, errors: new List<string> { "Error"});
        }
    }
}

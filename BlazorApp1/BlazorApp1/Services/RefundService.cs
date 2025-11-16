using BlazorApp1.DTOs;
using ECommerceApp.DTOs.FeedbackDTOs;
using ECommerceApp.DTOs.RefundDTOs;
using static System.Net.WebRequestMethods;

namespace BlazorApp1.Services
{
    public class RefundService
    {
        private readonly HttpClient _http;

        public RefundService(HttpClient http)
        {
            _http = http;
        }


        public async Task<ApiResponse<List<PendingRefundResponseDTO>>> GetEligibleRefundsAsync()
        {
            var response = await _http.GetAsync($"api/Refunds/GetEligibleRefunds");
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<List<PendingRefundResponseDTO>>>();
            if (result == null)
            {
                return new ApiResponse<List<PendingRefundResponseDTO>>(
                    500,
                    errors: new List<string> { result.Errors.FirstOrDefault() ?? "No response from server." }
                );
            }
            if (!result.Success)
            {
                return new ApiResponse<List<PendingRefundResponseDTO>>(
                    500,
                    errors: new List<string> { result.Errors.FirstOrDefault() ?? "No response from server." }
                );
            }
            return result;
        }
    }
}

using BlazorApp1.DTOs;
using ECommerceApp.DTOs.CancellationDTOs;
using ECommerceApp.DTOs.PaymentDTOs;

namespace BlazorApp1.Services
{
    public class CancellationService
    {
        private readonly HttpClient _http;

        public CancellationService(HttpClient http)
        {
            _http = http;
        }

        public async Task<ApiResponse<CancellationResponseDTO>> CancelOrderAsync(CancellationRequestDTO cancel)
        {
            var response = await _http.PostAsJsonAsync($"api/Cancellations/RequestCancellation", cancel);
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<CancellationResponseDTO>>();

            if (result == null)
            {
                return new ApiResponse<CancellationResponseDTO>(
                    500,
                    errors: new List<string> { result?.Errors.FirstOrDefault() ?? "Unknowm errorr" }
                );
            }
            if (!result.Success)
            {
                return new ApiResponse<CancellationResponseDTO>(
                    500,
                    errors: new List<string> { result.Errors.FirstOrDefault() ?? "Unknown errorr" }
                );
            }

            return result;
        }
    }
}

using BlazorApp1.DTOs;
using ECommerceApp.DTOs.OrderDTOs;
using ECommerceApp.DTOs.PaymentDTOs;

namespace BlazorApp1.Services
{
    public class PaymentService
    {
        private readonly HttpClient _http;

        public PaymentService(HttpClient http)
        {
            _http = http;
        }

        public async Task<ApiResponse<PaymentResponseDTO>> ProcessPaymentAsync(PaymentRequestDTO payment)
        {
            var response = await _http.PostAsJsonAsync($"api/Payments/ProcessPayment", payment);
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<PaymentResponseDTO>>();

            if (result == null)
            {
                return new ApiResponse<PaymentResponseDTO>(
                    500,
                    errors: new List<string> { result.Errors.FirstOrDefault() ?? "Unknowm errorr" }
                );
            }
            if (!result.Success)
            {
                return new ApiResponse<PaymentResponseDTO>(
                    500,
                    errors: new List<string> { result.Errors.FirstOrDefault() ?? "Unknown errorr" }
                );
            }

            return result;
        }
    }
}

using BlazorApp1.DTOs;
using EcommerceApp.DTOs;
using EcommerceApp.DTOs.PaymentDTO;
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

        public async Task<ApiResponse<PaymentResponseDTO>> GetPaymentByOrderId(int Id)
        {
            var response = await _http.GetAsync($"api/Payments/GetPaymentByOrderId/{Id}");
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

        public async Task<ApiResponse<StripeResponseDTO>> CreateCheckoutSession(StripeCheckoutDTO req)
        {
           //var json = new StripeCheckoutDTO
           //{
           //   Currency = "usd",
           //   ProductName = "Eat that frog",
           //   UnitPrice = 500,
           //   Quantity = 1,
           //  BaseUrl = "https://localhost:7094"
           // }; 
        var response = await _http.PostAsJsonAsync("api/Payments/create-stripe-session",req);

            if (!response.IsSuccessStatusCode)
                return null;

            var result = await response.Content.ReadFromJsonAsync<ApiResponse<StripeResponseDTO>>();
            var responseData = new StripeResponseDTO
            {
                Url = result?.Data.Url
            };
            return new ApiResponse<StripeResponseDTO>(200, responseData);
        }
        //public async Task<ApiResponse<StripeResponseDTO> CreateCheckoutSession(StripeCheckoutDTO req)
        //{
        //    var response = await _http.PostAsJsonAsync("api/Payments/StripeCheckout", req);

        //    if (!response.IsSuccessStatusCode)
        //        return null;

        //    var body = await response.Content.ReadFromJsonAsync<ApiResponse<StripeResponseDTO>>();

        //    return body?.Data?.Url;
        //}
    }
}

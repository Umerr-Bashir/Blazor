using BlazorApp1.DTOs;
using ECommerceApp.DTOs.FeedbackDTOs;
using ECommerceApp.DTOs.OrderDTOs;

namespace BlazorApp1.Services
{
    public class FeedbackService
    {
        private readonly HttpClient _http;

        public FeedbackService(HttpClient http)
        {
                _http = http;
        }

        public async Task<ApiResponse<FeedbackResponseDTO>> RequestFeedback(FeedbackCreateDTO feedback)
        {
            var response = await _http.PostAsJsonAsync($"api/Feedback/SubmitFeedback", feedback);
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<FeedbackResponseDTO>>();
            if (result == null)
            {
                return new ApiResponse<FeedbackResponseDTO>(
                    500,
                    errors: new List<string> { result.Errors.FirstOrDefault() ?? "No response from server." }
                );
            }
            if (!result.Success)
            {
                return new ApiResponse<FeedbackResponseDTO>(
                    500,
                    errors: new List<string> { result.Errors.FirstOrDefault() ?? "No response from server." }
                );
            }
            return result;
        }
    }
}

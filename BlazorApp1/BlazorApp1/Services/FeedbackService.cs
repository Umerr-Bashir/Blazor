using BlazorApp1.DTOs;
using ECommerceApp.DTOs.FeedbackDTOs;
using ECommerceApp.DTOs.OrderDTOs;
using ECommerceApp.DTOs.RefundDTOs;

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

        public async Task<ApiResponse<List<FeedbackResponseDTO>>> GetAllFeedbackAsync()
        {
            var response = await _http.GetAsync($"api/Feedback/GetAllFeedback");
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<List<FeedbackResponseDTO>>>();
            if (result == null)
            {
                return new ApiResponse<List<FeedbackResponseDTO>>(
                    500,
                    errors: new List<string> { result.Errors.FirstOrDefault() ?? "No response from server." }
                );
            }
            if (!result.Success)
            {
                return new ApiResponse<List<FeedbackResponseDTO>>(
                    500,
                    errors: new List<string> { result.Errors.FirstOrDefault() ?? "No response from server." }
                );
            }
            return result;
        }
        public async Task<ApiResponse<FeedbackResponseDTO>> DeleteFeedbackAsync(FeedbackDeleteDTO delete)
        {
            var response = await _http.PostAsJsonAsync($"api/Feedback/DeleteFeedback", delete);
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

        public async Task<ApiResponse<ProductFeedbackResponseDTO>> ProductFeedbackAsync(int Id)
        {
            var response = await _http.GetAsync($"api/Feedback/GetFeedbackForProductSpecifically/{Id}");
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<ProductFeedbackResponseDTO>>();
            if (result == null)
            {
                return new ApiResponse<ProductFeedbackResponseDTO>(
                    500,
                    errors: new List<string> { result.Errors.FirstOrDefault() ?? "No response from server." }
                );
            }
            if (!result.Success)
            {
                return new ApiResponse<ProductFeedbackResponseDTO>(
                    500,
                    errors: new List<string> { result.Errors.FirstOrDefault() ?? "No response from server." }
                );
            }
            return result;
        }


    }
}

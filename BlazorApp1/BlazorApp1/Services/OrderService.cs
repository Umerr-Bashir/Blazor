using BlazorApp1.Models;

namespace BlazorApp1.Services
{
    public class OrderService
    {
        private readonly HttpClient _http;

        public OrderService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<OrderResponseDTO>?> GetAllOrders()
        {
            var response = await _http.GetFromJsonAsync<ApiResponse<List<OrderResponseDTO>>>($"api/Order/GetAll");
            return response?.Data;
        }
    }
}

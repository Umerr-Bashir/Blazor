using ECommerceApp.Models;
namespace ECommerceApp.DTOs.RefundDTOs
{
    // Helper class to simulate a payment gateway response.
    public class PaymentGatewayRefundResponseDTO
    {
        public bool IsSuccess { get; set; }
        public RefundStatus Status { get; set; }
        public string TransactionId { get; set; }
    }
}
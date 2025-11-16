using ECommerceApp.Models;
namespace ECommerceApp.DTOs.RefundDTOs
{
    // DTO for returning refund details.
    public class RefundResponseDTO
    {
        public int Id { get; set; }
        public int CancellationId { get; set; }
        public int PaymentId { get; set; }
        public decimal Amount { get; set; }
        public RefundStatus Status { get; set; }
        public DateTime InitiatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public string? TransactionId { get; set; }
        public RefundMethod RefundMethod { get; set; }
        public string? RefundReason { get; set; }
    }
}
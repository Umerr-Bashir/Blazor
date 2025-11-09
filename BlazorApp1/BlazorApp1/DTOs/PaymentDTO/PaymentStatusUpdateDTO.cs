using ECommerceApp.Models;
using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.DTOs.PaymentDTOs
{
    // DTO for updating payment status
    public class PaymentStatusUpdateDTO
    {
        [Required(ErrorMessage = "Payment ID is required.")]
        public int PaymentId { get; set; }

        public string? TransactionId { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public PaymentStatus Status { get; set; }
    }
}
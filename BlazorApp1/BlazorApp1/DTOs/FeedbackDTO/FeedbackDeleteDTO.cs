using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.DTOs.FeedbackDTOs
{
    public class FeedbackDeleteDTO
    {
        [Required(ErrorMessage = "FeedbackId is required.")]
        public int FeedbackId { get; set; }

        [Required(ErrorMessage = "CustomerId is required.")]
        public int CustomerId { get; set; }
    }
}
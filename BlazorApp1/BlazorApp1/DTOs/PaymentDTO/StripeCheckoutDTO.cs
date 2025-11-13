namespace EcommerceApp.DTOs.PaymentDTO
{
    public class StripeCheckoutDTO
    {
        public string? Currency { get; set; }
        public string ProductName { get; set; }
        public long UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string BaseUrl { get; set; }
        public int CustomerId { get; set; }
        public int OrderId { get; set; }
        public string PaymentMethod { get; set; }
    }
}

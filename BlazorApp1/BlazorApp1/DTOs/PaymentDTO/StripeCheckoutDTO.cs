namespace EcommerceApp.DTOs.PaymentDTO
{
    public class StripeCheckoutDTO
    {
        public string Currency { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity{ get; set; }
        public string BaseUrl { get; set; }
    }
}

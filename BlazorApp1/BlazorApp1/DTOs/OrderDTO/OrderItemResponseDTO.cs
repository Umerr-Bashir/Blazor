namespace ECommerceApp.DTOs.OrderDTOs
{
    // DTO for returning individual order item details.
    public class OrderItemResponseDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
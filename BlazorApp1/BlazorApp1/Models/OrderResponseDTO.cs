namespace BlazorApp1.Models
{
    public class OrderResponseDTO
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public List<OrderDetailResponseDTO> OrderDetails { get; set; }
        public double GrandTotal { get; set; }
    }
}

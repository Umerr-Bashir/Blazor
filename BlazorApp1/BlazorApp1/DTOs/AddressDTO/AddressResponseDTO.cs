namespace ECommerceApp.DTOs.AddressesDTOs
{
    // DTO for returning address details.
    public class AddressResponseDTO
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string PresentAddress { get; set; }
        public string PermanentAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.DTOs.AddressesDTOs
{
    public class AddressDeleteDTO
    {
        [Required(ErrorMessage = "CustomerId is Required")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "AddressId is Required")]
        public int AddressId { get; set; }
    }
}
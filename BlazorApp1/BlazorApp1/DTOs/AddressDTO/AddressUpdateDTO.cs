using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceApp.DTOs.AddressesDTOs
{
    public class AddressUpdateDTO
    {
        [Required(ErrorMessage = "AddressId is required.")]
        public int AddressId { get; set; }

        [Required(ErrorMessage = "CustomerId is required.")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Address Line 1 is required.")]
        [StringLength(100, ErrorMessage = "Address Line 1 cannot exceed 100 characters.")]
        public string PresentAddress { get; set; }

        [Required(ErrorMessage = "Address Line 2 is required.")]

        [StringLength(100, ErrorMessage = "Address Line 2 cannot exceed 100 characters.")]
        public string PermanentAddress { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [StringLength(50, ErrorMessage = "City cannot exceed 50 characters.")]
        public string City { get; set; }

        [Required(ErrorMessage = "State is required.")]
        [StringLength(50, ErrorMessage = "State cannot exceed 50 characters.")]
        public string State { get; set; }

        [Required(ErrorMessage = "Postal Code is required.")]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "Postal code is required")]
        public string PostalCode { get; set; }


        [Required(ErrorMessage = "Country is required.")]
        [StringLength(50, ErrorMessage = "Country cannot exceed 50 characters.")]
        [Column(TypeName = "nvarchar(50)")]
        public string Country { get; set; }
    }
}
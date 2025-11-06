using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceApp.DTOs.AddressDTO
{
    public class AddressCreateDTO
    {
        [Required(ErrorMessage = "CustomerId is required.")]
        public int CustomerId { get; set; }


        [Required(ErrorMessage = "Present Address is required.")]
        [StringLength(40, ErrorMessage = "Present Address cannot exceed 100 characters.")]
        public string PresentAddress { get; set; }


        [Required(ErrorMessage = "Permanent Address is required.")]
        [StringLength(40, ErrorMessage = "Permanent Address cannot exceed 100 characters.")]
        public string PermanentAddress { get; set; }


        [Required(ErrorMessage = "State is required.")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "State is required")]
        public string State { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "City is required")]
        public string City { get; set; }


        [Required(ErrorMessage = "Postal code is required.")]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "Postal code is required")]
        public string PostalCode { get; set; }


        [Required(ErrorMessage = "Country is required.")]
        [StringLength(50, ErrorMessage = "Country cannot exceed 50 characters.")]
        [Column(TypeName = "nvarchar(50)")]
        public string Country { get; set; }


    }
}

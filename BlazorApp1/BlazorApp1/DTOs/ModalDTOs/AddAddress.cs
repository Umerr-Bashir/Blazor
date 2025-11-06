using System.ComponentModel.DataAnnotations;

namespace BlazorApp1.DTOs.ModalDTOs
{
    public class AddAddress
    {
        [Required(ErrorMessage = "Present address is required.")]
        [StringLength(100, ErrorMessage = "Address too long.")]
        public string PresentAddress { get; set; } = "";

        [Required(ErrorMessage = "Permanent address is required.")]
        [StringLength(100, ErrorMessage = "Address too long.")]

        public string PermanentAddress { get; set; } = "";

        [Required(ErrorMessage = "State is required.")]
        [StringLength(50, ErrorMessage = "State too long.")]

        public string State { get; set; } = "";

        [Required(ErrorMessage = "City is required.")]
        [StringLength(50, ErrorMessage = "City too long.")]
        public string City { get; set; } = "";

        [Required(ErrorMessage = "Postal Code is required.")]
        [StringLength(6, ErrorMessage = "Postal Code too long.")]
        public string PostalCode { get; set; } = "";

        [Required(ErrorMessage = "Country is required.")]
        [StringLength(50, ErrorMessage = "Country too long.")]
        public string Country { get; set; } = "";
    }
}

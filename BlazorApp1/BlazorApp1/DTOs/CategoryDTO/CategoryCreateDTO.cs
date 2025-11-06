using System.ComponentModel.DataAnnotations;
namespace ECommerceApp.DTOs.CategoryDTOs
{

    public class CategoryCreateDTO
    {
        [Required(ErrorMessage = "Category Name is required.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Category Name must be between 3 and 30 characters.")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }
    }
}
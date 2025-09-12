using System.ComponentModel.DataAnnotations;

namespace BlazorE_CommerceApp.Dtos.CategoryDtos
{
    public class CreateCategoryDto
    {
        [Required(ErrorMessage = "Category Name is required.")]
        public string CategoryName { get; set; }
    }
}

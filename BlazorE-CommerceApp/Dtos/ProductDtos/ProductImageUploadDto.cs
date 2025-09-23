using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace BlazorE_CommerceApp.Dtos.ProductDtos
{
    public class ProductImageUploadDto
    {

        [Required]
        public IBrowserFile File { get; set; }

    }
}

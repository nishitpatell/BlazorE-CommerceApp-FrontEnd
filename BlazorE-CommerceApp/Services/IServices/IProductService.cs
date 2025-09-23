using BlazorE_CommerceApp.Dtos.ProductDtos;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorE_CommerceApp.Services.IServices
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<ProductDto> CreateProductAsync(CreateProductDto createProductDto);
        Task<ProductDto> UpdateProductAsync(UpdateProductDto updateProductDto);
        Task<bool> DeleteProductAsync(int id);
        Task<(bool IsSuccess, string ImageUrl, string ErrorMessage)> UploadProductImageAsync(int productId, IBrowserFile file);
    }
}

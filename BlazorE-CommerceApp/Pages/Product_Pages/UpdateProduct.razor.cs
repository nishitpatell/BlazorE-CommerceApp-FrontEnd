using BlazorE_CommerceApp.Dtos.CategoryDtos;
using BlazorE_CommerceApp.Dtos.ProductDtos;
using BlazorE_CommerceApp.Helper.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;

namespace BlazorE_CommerceApp.Pages.Product_Pages
{
    public partial class UpdateProduct
    {
        [Parameter]
        public int Id { get; set; }
        [SupplyParameterFromForm(FormName = "updateProductForm")]
        private UpdateProductDto updateProductDto { get; set; } = new UpdateProductDto();
        private bool IsProcessing { get; set; } = true;

        private IEnumerable<CategoryDto> _categories = new List<CategoryDto>();

        private ProductImageUploadDto imageUploadDto = new ProductImageUploadDto();
        private string StatusMessage;
        private string UploadedImageUrl;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await LoadProductAndCategoryList();
                IsProcessing = false;
                StateHasChanged();
            }
        }

        private async Task LoadProductAndCategoryList()
        {
            if (Id > 0)
            {
                var productDto = await _productService.GetProductByIdAsync(Id);
                _categories = await _categoryService.GetAllCategoriesAsync();
                updateProductDto.ProductId = productDto.ProductId;
                updateProductDto.ProductName = productDto.ProductName;
                updateProductDto.ProductDescription = productDto.ProductDescription;
                updateProductDto.CategoryId = productDto.CategoryId;
                updateProductDto.ProductPrice = productDto.ProductPrice;
            }
        }

        private async Task EditProduct()
        {
            IsProcessing = true;
            if (updateProductDto.ProductId > 0)
            {
                await _productService.UpdateProductAsync(updateProductDto);
                //await UploadProductImage();
                await _JSRuntime.ToastrSuccess("Product Updated Successfully!");

                IsProcessing = false;
            }
            _navigationManager.NavigateTo("product");
        }

        private async Task OnFileSelected(InputFileChangeEventArgs e)
        {
            var file = e.File;

            if (file.Size > 5 * 1024 * 1024)
            {
                StatusMessage = "File size must be <= 5MB.";
                return;
            }

            if (!new[] { "image/png", "image/jpeg" }.Contains(file.ContentType))
            {
                StatusMessage = "Only PNG/JPEG allowed.";
                return;
            }

            // Process immediately: upload directly from here
            var result = await _productService.UploadProductImageAsync(Id, file);
            //var result = await _productService.UploadProductImageAsync(updateProductDto.ProductId, file);

            if (result.IsSuccess)
            {
                UploadedImageUrl = result.ImageUrl;

                string marker = @"product/productImages/";

                // Find the index of the marker in the full path
                int index = UploadedImageUrl.IndexOf(marker, StringComparison.OrdinalIgnoreCase);

                string relativePath = index >= 0 ? UploadedImageUrl.Substring(index) : string.Empty;

                UploadedImageUrl = relativePath;
                updateProductDto.ProductImageUrl = UploadedImageUrl;
                StatusMessage = "Image uploaded successfully!";
            }
            else
            {
                StatusMessage = $"Upload failed: {result.ErrorMessage}";
            }
        }
    }
}
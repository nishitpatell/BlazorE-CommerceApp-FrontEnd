using BlazorE_CommerceApp.Dtos.CategoryDtos;
using BlazorE_CommerceApp.Dtos.ProductDtos;
using BlazorE_CommerceApp.Helper.Extensions;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorE_CommerceApp.Pages.Product_Pages
{
    public partial class CreateProduct
    {
        private CreateProductDto createProductDto { get; set; } = new CreateProductDto();
        private bool IsProcessing { get; set; } = true;

        private IEnumerable<CategoryDto> _categories = new List<CategoryDto>();

        private string? _directoryPath { get; set; }

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
            await Task.Delay(300);
            _categories = await _categoryService.GetAllCategoriesAsync();
        }

        private async Task AddNewProduct()
        {
            IsProcessing = true;
            await _productService.CreateProductAsync(createProductDto);
            await _JSRuntime.ToastrSuccess("Product Created Successfully!");
            IsProcessing = false;
            _navigationManager.NavigateTo("product");
        }

        private async Task LoadFiles(InputFileChangeEventArgs e)
        {

        }
    }
}
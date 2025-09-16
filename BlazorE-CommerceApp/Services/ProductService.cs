using BlazorE_CommerceApp.Dtos.CategoryDtos;
using BlazorE_CommerceApp.Dtos.ProductDtos;
using BlazorE_CommerceApp.Services.IServices;
using System.Net.Http.Json;

namespace BlazorE_CommerceApp.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _http;

        public ProductService(HttpClient http)
        {
            _http = http;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            return await _http.GetFromJsonAsync<IEnumerable<ProductDto>>("api/Product");
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {

            return await _http.GetFromJsonAsync<ProductDto>($"api/Product/{id}");
        }
        public async Task<ProductDto> CreateProductAsync(CreateProductDto createProductDto)
        {
            var response = await _http.PostAsJsonAsync("api/Product", createProductDto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ProductDto>();
        }

        public async Task<ProductDto> UpdateProductAsync(UpdateProductDto updateProductDto)
        {
            var response = await _http.PutAsJsonAsync($"api/Product/{updateProductDto.ProductId}", updateProductDto);
            try
            {
                return await response.Content.ReadFromJsonAsync<ProductDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/Produuct/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}

using BlazorE_CommerceApp.Dtos.CategoryDtos;
using BlazorE_CommerceApp.Dtos.ProductDtos;
using BlazorE_CommerceApp.Services.IServices;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

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

        public async Task<(bool IsSuccess, string ImageUrl, string ErrorMessage)> UploadProductImageAsync(int productId, IBrowserFile file)
        {
            try
            {
                var content = new MultipartFormDataContent();
                var fileStream = file.OpenReadStream(5 * 1024 * 1024);

                var fileContent = new StreamContent(fileStream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);

                content.Add(fileContent, "File", file.Name);

                var response = await _http.PostAsync($"api/product/{productId}/image", content);

                if(response.IsSuccessStatusCode)
                {
                    string imageUrl = await response.Content.ReadAsStringAsync();
                    return (true, imageUrl, null);
                }

                var error = await response.Content.ReadAsStringAsync();
                return(false, null, error);
            }
            catch(Exception ex) 
            {
                return (false, null, ex.Message); 
            }
        }
    }
}

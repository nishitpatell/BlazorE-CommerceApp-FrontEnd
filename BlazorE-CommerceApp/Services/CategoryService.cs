using BlazorE_CommerceApp.Dtos.CategoryDtos;
using BlazorE_CommerceApp.Services.IServices;
using System.Net.Http.Json;

namespace BlazorE_CommerceApp.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _http;

        public CategoryService(HttpClient http)
        {
            _http = http;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            return await _http.GetFromJsonAsync<IEnumerable<CategoryDto>>("api/Category");
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(int id)
        {
            return await _http.GetFromJsonAsync<CategoryDto>($"api/Category/{id}");
        }

        public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            var response = await _http.PostAsJsonAsync("api/Category", createCategoryDto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CategoryDto>();
        }


        public async Task<CategoryDto> UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto)
        {
            var response = await _http.PutAsJsonAsync($"api/Category/{updateCategoryDto.CategoryId}", updateCategoryDto);
            response.EnsureSuccessStatusCode();
            try
            {
                return await response.Content.ReadFromJsonAsync<CategoryDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/Category/{id}");
            return response.IsSuccessStatusCode;
        }   
    }
}

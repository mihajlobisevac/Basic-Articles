using BasicArticles.Shared;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BasicArticles.Client.ViewModels.Category
{
    public class CategoryViewModel : ICategoryViewModel
    {
        private HttpClient HttpClient { get; set; }
        public CategoryViewModel(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        [Key]
        public long Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public CategoryViewModel()
        {
        }

        public static implicit operator CategoryViewModel(CategoryModel i)
        {
            return new CategoryViewModel
            {
                Id = i.Id,
                Name = i.Name
            };
        }
        public static implicit operator CategoryModel(CategoryViewModel i)
        {
            return new CategoryModel
            {
                Id = i.Id,
                Name = i.Name
            };
        }

        public async Task CreateCategory(CategoryModel model)
        {
            await HttpClient.PostAsJsonAsync("Category", model);
        }

        public async Task DeleteCategory(long id)
        {
            await HttpClient.DeleteAsync($"Category/{id}");
        }

        public async Task<CategoryModel> GetCategory(long id)
        {
            return await HttpClient.GetFromJsonAsync<CategoryModel>($"Category/{id}");
        }

        public async Task<List<CategoryModel>> GetCategoryList()
        {
            return await HttpClient.GetFromJsonAsync<List<CategoryModel>>("Category");
        }

        public async Task UpdateCategory(long id, CategoryModel model)
        {
            await HttpClient.PutAsJsonAsync($"Category/{id}", model);
        }
    }
}

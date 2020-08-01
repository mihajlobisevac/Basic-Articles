using BasicArticles.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BasicArticles.Client.ViewModels.Category
{
    public interface ICategoryViewModel
    {
        Task<List<CategoryModel>> GetCategoryList();
        Task<CategoryModel> GetCategory(long id);
        Task CreateCategory(CategoryModel model);
        Task UpdateCategory(long id, CategoryModel model);
        Task DeleteCategory(long id);
    }
}

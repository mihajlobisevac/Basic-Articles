using BasicArticles.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicArticles.Server.Data.Category
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryModel>> Search(string name);
        Task<IEnumerable<CategoryModel>> GetCategoryList();
        Task<CategoryModel> GetCategory(long id);
        Task<CategoryModel> AddCategory(CategoryModel model);
        Task<CategoryModel> UpdateCategory(CategoryModel model);
        Task<CategoryModel> DeleteCategory(long id);
    }
}

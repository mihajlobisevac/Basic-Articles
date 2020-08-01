using Microsoft.EntityFrameworkCore;
using BasicArticles.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicArticles.Server.Data.Category
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CategoryDbContext dbContext;

        public CategoryRepository(CategoryDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task<CategoryModel> AddCategory(CategoryModel model)
        {
            var result = await dbContext.Categories.AddAsync(model);
            await dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<CategoryModel> DeleteCategory(long id)
        {
            var result = await dbContext.Categories
                .FirstOrDefaultAsync(i => i.Id == id);
            if (result != null)
            {
                dbContext.Categories.Remove(result);
                await dbContext.SaveChangesAsync();
                return result;
            }

            return null;
        }

        public async Task<CategoryModel> GetCategory(long id)
        {
            return await dbContext.Categories.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<CategoryModel>> GetCategoryList()
        {
            return await dbContext.Categories.ToListAsync();
        }

        public async Task<IEnumerable<CategoryModel>> Search(string name)
        {
            IQueryable<CategoryModel> query = dbContext.Categories;

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(i => i.Name.Contains(name));
            }

            return await query.ToListAsync();
        }

        public async Task<CategoryModel> UpdateCategory(CategoryModel model)
        {
            var result = await dbContext.Categories
                .FirstOrDefaultAsync(i => i.Id == model.Id);

            if (result != null)
            {
                result.Name = model.Name;

                await dbContext.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}

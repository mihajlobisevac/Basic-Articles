using Microsoft.EntityFrameworkCore;
using BasicArticles.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicArticles.Server.Data.Article
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly ArticleDbContext dbContext;

        public ArticleRepository(ArticleDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task<ArticleModel> AddArticle(ArticleModel model)
        {
            var result = await dbContext.Articles.AddAsync(model);
            await dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<ArticleModel> DeleteArticle(long id)
        {
            var result = await dbContext.Articles
                .FirstOrDefaultAsync(i => i.Id == id);
            if (result != null)
            {
                dbContext.Articles.Remove(result);
                await dbContext.SaveChangesAsync();
                return result;
            }

            return null;
        }

        public async Task<ArticleModel> GetArticle(long id)
        {
            return await dbContext.Articles.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<ArticleModel>> GetArticleList()
        {
            return await dbContext.Articles.ToListAsync();
        }

        public async Task<List<ArticleModel>> GetArticleListByCategory(string category)
        {
            return await dbContext
                            .Articles
                            .Where(i => i.Category.ToLower().Contains(category.ToLower()))
                            .ToListAsync();
        }

        public async Task<IEnumerable<ArticleModel>> Search(string name)
        {
            IQueryable<ArticleModel> query = dbContext.Articles;

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(i => i.Title.Contains(name));
            }

            return await query.ToListAsync();
        }

        public async Task<ArticleModel> UpdateArticle(ArticleModel model)
        {
            var result = await dbContext.Articles
                .FirstOrDefaultAsync(i => i.Id == model.Id);

            if (result != null)
            {
                result.Title = model.Title;
                result.IntroText = model.IntroText;
                result.BodyText = model.BodyText;
                result.ImagePath = model.ImagePath;
                result.UpdatedDate = model.UpdatedDate;
                result.Category = model.Category;

                await dbContext.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}

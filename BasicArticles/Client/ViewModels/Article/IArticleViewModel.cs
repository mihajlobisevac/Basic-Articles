using BasicArticles.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BasicArticles.Client.ViewModels.Article
{
    public interface IArticleViewModel
    {
        Task<List<ArticleModel>> GetArticleList();
        Task<List<ArticleModel>> GetArticleListByCategory(string category);
        Task<List<ArticleModel>> GetArticleListByUser(string user);
        Task<ArticleModel> GetArticle(long id);
        Task CreateArticle(ArticleModel model);
        Task UpdateArticle(long id, ArticleModel model);
        Task DeleteArticle(long id);
    }
}

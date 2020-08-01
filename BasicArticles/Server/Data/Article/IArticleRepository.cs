using BasicArticles.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicArticles.Server.Data.Article
{
    public interface IArticleRepository
    {
        Task<IEnumerable<ArticleModel>> Search(string name);
        Task<IEnumerable<ArticleModel>> GetArticleList();
        Task<List<ArticleModel>> GetArticleListByCategory(string category);
        Task<List<ArticleModel>> GetArticleListByUser(string user);
        Task<ArticleModel> GetArticle(long id);
        Task<ArticleModel> AddArticle(ArticleModel model);
        Task<ArticleModel> UpdateArticle(ArticleModel model);
        Task<ArticleModel> DeleteArticle(long id);
    }
}

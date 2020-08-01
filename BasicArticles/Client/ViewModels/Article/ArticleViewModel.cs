using BasicArticles.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BasicArticles.Client.ViewModels.Article
{
    public class ArticleViewModel : IArticleViewModel
    {
        private HttpClient HttpClient { get; set; }
        public ArticleViewModel(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        [Key]
        public long Id { get; set; }
        [MaxLength(150)]
        public string Title { get; set; }
        [MaxLength(300)]
        public string IntroText { get; set; }
        [MaxLength(3000)]
        public string BodyText { get; set; }
        [MaxLength(100)]
        public string ImagePath { get; set; }
        public DateTime PublishedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Category { get; set; }
        public string User { get; set; }

        public ArticleViewModel()
        {
        }

        public static implicit operator ArticleViewModel(ArticleModel i)
        {
            return new ArticleViewModel
            {
                Id = i.Id,
                Title = i.Title,
                IntroText = i.IntroText,
                BodyText = i.BodyText,
                ImagePath = i.ImagePath,
                PublishedDate = i.PublishedDate,
                UpdatedDate = i.UpdatedDate,
                Category = i.Category,
                User = i.User
            };
        }
        public static implicit operator ArticleModel(ArticleViewModel i)
        {
            return new ArticleModel
            {
                Id = i.Id,
                Title = i.Title,
                IntroText = i.IntroText,
                BodyText = i.BodyText,
                ImagePath = i.ImagePath,
                PublishedDate = i.PublishedDate,
                UpdatedDate = i.UpdatedDate,
                Category = i.Category,
                User = i.User
            };
        }

        public async Task CreateArticle(ArticleModel model)
        {
            await HttpClient.PostAsJsonAsync("Article", model);
        }

        public async Task DeleteArticle(long id)
        {
            await HttpClient.DeleteAsync($"Article/{id}");
        }

        public async Task<ArticleModel> GetArticle(long id)
        {
            return await HttpClient.GetFromJsonAsync<ArticleModel>($"Article/{id}");
        }

        public async Task<List<ArticleModel>> GetArticleList()
        {
            return await HttpClient.GetFromJsonAsync<List<ArticleModel>>("Article");
        }
        public async Task<List<ArticleModel>> GetArticleListByCategory(string category)
        {
            return await HttpClient.GetFromJsonAsync<List<ArticleModel>>($"Article/Category/{category}");
        }

        public async Task UpdateArticle(long id, ArticleModel model)
        {
            await HttpClient.PutAsJsonAsync($"Article/{id}", model);
        }
    }
}

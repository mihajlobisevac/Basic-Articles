using BasicArticles.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BasicArticles.Client.ViewModels.Comment
{
    public class CommentViewModel : ICommentViewModel
    {
        private HttpClient HttpClient { get; set; }
        public CommentViewModel(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        [Key]
        public long Id { get; set; }
        [MaxLength(500, ErrorMessage = "Comment must contain at most 500 characters")]
        [MinLength(2, ErrorMessage = "Comment must contain at least 2 characters")]
        public string BodyText { get; set; }
        public DateTime PublishedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public long Article { get; set; }
        public string User { get; set; }

        public CommentViewModel()
        {
        }

        public static implicit operator CommentViewModel(CommentModel i)
        {
            return new CommentViewModel
            {
                Id = i.Id,
                BodyText = i.BodyText,
                PublishedDate = i.PublishedDate,
                UpdatedDate = i.UpdatedDate,
                Article = i.Article,
                User = i.User
            };
        }
        public static implicit operator CommentModel(CommentViewModel i)
        {
            return new CommentModel
            {
                Id = i.Id,
                BodyText = i.BodyText,
                PublishedDate = i.PublishedDate,
                UpdatedDate = i.UpdatedDate,
                Article = i.Article,
                User = i.User
            };
        }

        public async Task CreateComment(CommentModel model)
        {
            await HttpClient.PostAsJsonAsync("Comment", model);
        }

        public async Task DeleteComment(long id)
        {
            await HttpClient.DeleteAsync($"Comment/{id}");
        }

        public async Task<CommentModel> GetComment(long id)
        {
            return await HttpClient.GetFromJsonAsync<CommentModel>($"Comment/{id}");
        }

        public async Task<List<CommentModel>> GetCommentList()
        {
            return await HttpClient.GetFromJsonAsync<List<CommentModel>>("Comment");
        }

        public async Task<List<CommentModel>> GetCommentListByArticle(long id)
        {
            return await HttpClient.GetFromJsonAsync<List<CommentModel>>($"Comment/list/{id}");
        }

        public async Task<List<CommentModel>> GetCommentListByUser(string user)
        {
            return await HttpClient.GetFromJsonAsync<List<CommentModel>>($"Comment/user/{user}");
        }

        public async Task UpdateComment(long id, CommentModel model)
        {
            await HttpClient.PutAsJsonAsync($"Comment/{id}", model);
        }
    }
}

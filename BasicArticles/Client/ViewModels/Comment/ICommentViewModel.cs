using BasicArticles.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BasicArticles.Client.ViewModels.Comment
{
    public interface ICommentViewModel
    {
        Task<List<CommentModel>> GetCommentList();
        Task<List<CommentModel>> GetCommentListByArticle(long id);
        Task<CommentModel> GetComment(long id);
        Task CreateComment(CommentModel model);
        Task UpdateComment(long id, CommentModel model);
        Task DeleteComment(long id);
    }
}

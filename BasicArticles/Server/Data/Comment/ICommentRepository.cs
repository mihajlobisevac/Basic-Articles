using BasicArticles.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicArticles.Server.Data.Comment
{
    public interface ICommentRepository
    {
        Task<IEnumerable<CommentModel>> Search(string name);
        Task<IEnumerable<CommentModel>> GetCommentList();
        Task<IEnumerable<CommentModel>> GetCommentListByArticle(long id);
        Task<IEnumerable<CommentModel>> GetCommentListByUser(string user);
        Task<CommentModel> GetComment(long id);
        Task<CommentModel> AddComment(CommentModel model);
        Task<CommentModel> UpdateComment(CommentModel model);
        Task<CommentModel> DeleteComment(long id);
    }
}

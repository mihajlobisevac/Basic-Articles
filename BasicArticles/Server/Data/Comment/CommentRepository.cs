using Microsoft.EntityFrameworkCore;
using BasicArticles.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicArticles.Server.Data.Comment
{
    public class CommentRepository : ICommentRepository
    {
        private readonly CommentDbContext dbContext;

        public CommentRepository(CommentDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task<CommentModel> AddComment(CommentModel model)
        {
            var result = await dbContext.Comments.AddAsync(model);
            await dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<CommentModel> DeleteComment(long id)
        {
            var result = await dbContext.Comments
                .FirstOrDefaultAsync(i => i.Id == id);
            if (result != null)
            {
                dbContext.Comments.Remove(result);
                await dbContext.SaveChangesAsync();
                return result;
            }

            return null;
        }

        public async Task<CommentModel> GetComment(long id)
        {
            return await dbContext.Comments.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<CommentModel>> GetCommentList()
        {
            return await dbContext.Comments.ToListAsync();
        }
        public async Task<IEnumerable<CommentModel>> GetCommentListByArticle(long id)
        {
            return await dbContext.Comments.Where(i => i.Article == id).ToListAsync();
        }

        public async Task<IEnumerable<CommentModel>> Search(string name)
        {
            IQueryable<CommentModel> query = dbContext.Comments;

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(i => i.BodyText.Contains(name));
            }

            return await query.ToListAsync();
        }

        public async Task<CommentModel> UpdateComment(CommentModel model)
        {
            var result = await dbContext.Comments
                .FirstOrDefaultAsync(i => i.Id == model.Id);

            if (result != null)
            {
                result.BodyText = model.BodyText;
                result.UpdatedDate = model.UpdatedDate;

                await dbContext.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}

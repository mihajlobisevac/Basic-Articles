using BasicArticles.Client.ViewModels.Article;
using BasicArticles.Client.ViewModels.Comment;
using BasicArticles.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicArticles.Client.Pages.Article
{
    public class ArticleBase : ComponentBase
    {
        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Parameter]
        public long Id { get; set; }

        [Inject]
        public IArticleViewModel ArticleService { get; set; }

        public ArticleModel ArticleModel { get; set; } = new ArticleModel();
        public string[] SplitBodyText { get; set; }


        [Inject]
        public ICommentViewModel CommentService { get; set; }
        public List<CommentModel> Comments { get; set; } = new List<CommentModel>();
        public CommentViewModel CommentViewModel { get; set; } = new CommentViewModel();
        public CommentModel CommentModel { get; set; } = new CommentModel();


        public string CommentAs { get; set; }

        protected override async Task OnInitializedAsync()
        {
            ArticleModel = await ArticleService.GetArticle(Id);
            SplitBodyText = ArticleModel.BodyText.Split("\n");

            Comments = await CommentService.GetCommentListByArticle(Id);

            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            CommentAs = authState.User.Identity.Name;
        }

        protected async Task HandleValidComment()
        {
            CommentViewModel.PublishedDate = DateTime.Now;
            CommentViewModel.UpdatedDate = DateTime.Now;
            CommentViewModel.User = CommentAs;
            CommentViewModel.Article = Id;

            CommentModel = CommentViewModel;

            await CommentService.CreateComment(CommentModel);

            CommentViewModel.BodyText = null;
            Comments = await CommentService.GetCommentListByArticle(Id);
        }

        protected async Task HandleEditComment()
        {

        }
        protected async Task HandleDeleteComment(long comment)
        {
            await CommentService.DeleteComment(comment);

            Comments = await CommentService.GetCommentListByArticle(Id);
        }
    }
}

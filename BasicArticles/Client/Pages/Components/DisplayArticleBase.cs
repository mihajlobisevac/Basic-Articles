using BasicArticles.Client.ViewModels.Comment;
using BasicArticles.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicArticles.Client.Pages.Components
{
    public class DisplayArticleBase : ComponentBase
    {
        [Inject]
        public NavigationManager Navigation { get; set; }
        [Inject]
        public ICommentViewModel CommentService { get; set; }

        [Parameter]
        public ArticleModel Article { get; set; }

        public List<CommentModel> Comments { get; set; }
        public string ArticleRoute { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Comments = await CommentService.GetCommentListByArticle(Article.Id);

            ArticleRoute = $"article/{Article.Id}";
        }
    }
}

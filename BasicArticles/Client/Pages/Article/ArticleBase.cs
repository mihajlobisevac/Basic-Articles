using BasicArticles.Client.ViewModels.Article;
using BasicArticles.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicArticles.Client.Pages.Article
{
    public class ArticleBase : ComponentBase
    {
        [Inject]
        public IArticleViewModel ArticleService { get; set; }

        [Parameter]
        public long Id { get; set; }

        public ArticleModel ArticleModel { get; set; } = new ArticleModel();

        protected override async Task OnInitializedAsync()
        {
            ArticleModel = await ArticleService.GetArticle(Id);
        }
    }
}

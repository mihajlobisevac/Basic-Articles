using BasicArticles.Client.ViewModels.Article;
using BasicArticles.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicArticles.Client.Pages
{
    public class IndexBase : ComponentBase
    {
        [Inject]
        public IArticleViewModel ArticleService { get; set; }

        public List<ArticleModel> Articles { get; set; } = new List<ArticleModel>();

        protected override async Task OnInitializedAsync()
        {
            Articles = await ArticleService.GetArticleList();
        }
    }
}

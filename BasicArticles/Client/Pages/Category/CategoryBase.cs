using BasicArticles.Client.ViewModels.Article;
using BasicArticles.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicArticles.Client.Pages.Category
{
    public class CategoryBase : ComponentBase
    {
        [Inject]
        protected IArticleViewModel ArticleViewModel { get; set; }
        [Parameter]
        public string CategoryName { get; set; }

        public List<ArticleModel> Articles { get; set; } = new List<ArticleModel>();

        protected override async Task OnInitializedAsync()
        {
            Articles = await ArticleViewModel.GetArticleListByCategory(CategoryName);
        }

        protected override async Task OnParametersSetAsync()
        {
            Articles = await ArticleViewModel.GetArticleListByCategory(CategoryName);

            StateHasChanged();
        }
    }
}

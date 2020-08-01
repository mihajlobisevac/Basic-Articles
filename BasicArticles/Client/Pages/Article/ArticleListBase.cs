using BasicArticles.Client.ViewModels.Article;
using BasicArticles.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicArticles.Client.Pages.Article
{
    public class ArticleListBase : ComponentBase
    {
        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject]
        protected IArticleViewModel ArticleService { get; set; }
        [Inject]
        protected NavigationManager Navigation { get; set; }

        public List<ArticleModel> ArticleList { get; set; } = new List<ArticleModel>();

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            ArticleList = await ArticleService.GetArticleListByUser(user.Identity.Name);
        }

        protected void Navigate(string route)
        {
            Navigation.NavigateTo(route);
        }

        protected async Task DeleteArticle(long id)
        {
            await ArticleService.DeleteArticle(id);
            StateHasChanged();
            Navigation.NavigateTo("manage");
        }
    }
}

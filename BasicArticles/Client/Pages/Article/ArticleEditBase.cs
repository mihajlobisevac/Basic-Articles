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
    public class ArticleEditBase : ComponentBase
    {
        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject]
        public IArticleViewModel ArticleService { get; set; }
        [Inject]
        public NavigationManager Navigation { get; set; }

        [Parameter]
        public long Id { get; set; }

        public ArticleModel ArticleModel { get; set; } = new ArticleModel();
        public ArticleViewModel ArticleViewModel { get; set; } = new ArticleViewModel();

        public string Username { get; set; }

        protected override async Task OnInitializedAsync()
        {
            ArticleModel = await ArticleService.GetArticle(Id);

            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            Username = authState.User.Identity.Name;

            ArticleViewModel = ArticleModel;
        }

        protected async Task HandleValidEdit()
        {
            ArticleModel = ArticleViewModel;

            await ArticleService.UpdateArticle(Id, ArticleModel);

            Navigation.NavigateTo("myarticles");
        }

        protected void Cancel_Click()
        {
            Navigation.NavigateTo("manage");
        }
    }
}

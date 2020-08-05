using BasicArticles.Client.ViewModels.Article;
using BasicArticles.Client.ViewModels.Category;
using BasicArticles.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BasicArticles.Client.Pages.Article
{
    public class ArticleCreateBase : ComponentBase
    {
        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject]
        public IArticleViewModel ArticleService { get; set; }
        [Inject]
        public ICategoryViewModel CategoryService { get; set; }
        [Inject]
        public NavigationManager Navigation { get; set; }

        public ArticleModel ArticleModel { get; set; } = new ArticleModel();
        public ArticleViewModel ArticleViewModel { get; set; } = new ArticleViewModel();
        public List<CategoryModel> Categories { get; set; } = new List<CategoryModel>();
        public List<CategoryViewModel> CategoriesViewModel { get; set; } = new List<CategoryViewModel>();

        protected override async Task OnInitializedAsync()
        {
            Categories = await CategoryService.GetCategoryList();

            foreach (var category in Categories)
            {
                CategoriesViewModel.Add(category);
            }
        }

        protected void Cancel_Click()
        {
            Navigation.NavigateTo("manage");
        }

        protected async Task HandleValidCreate()
        {
            ArticleViewModel.PublishedDate = DateTime.Now;
            ArticleViewModel.UpdatedDate = DateTime.Now;

            foreach (var item in CategoriesViewModel)
            {
                if (item.Selected == true && !ArticleViewModel.Category.Contains(item.Name))
                {
                    ArticleViewModel.Category += item.Name;
                }
            }

            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            ArticleViewModel.User = authState.User.Identity.Name;

            ArticleModel = ArticleViewModel;

            await ArticleService.CreateArticle(ArticleModel);

            Navigation.NavigateTo("manage");
        }
    }
}

using BasicArticles.Client.ViewModels.Article;
using BasicArticles.Client.ViewModels.Category;
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
        public ICategoryViewModel CategoryService { get; set; }
        [Inject]
        public NavigationManager Navigation { get; set; }

        [Parameter]
        public long Id { get; set; }

        public ArticleModel ArticleModel { get; set; } = new ArticleModel();
        public ArticleViewModel ArticleViewModel { get; set; } = new ArticleViewModel();
        public List<CategoryModel> Categories { get; set; } = new List<CategoryModel>();
        public List<CategoryViewModel> CategoriesViewModel { get; set; } = new List<CategoryViewModel>();

        public string Username { get; set; }

        protected override async Task OnInitializedAsync()
        {
            ArticleModel = await ArticleService.GetArticle(Id);

            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            Username = authState.User.Identity.Name;

            ArticleViewModel = ArticleModel;

            // category
            Categories = await CategoryService.GetCategoryList();

            foreach (var category in Categories)
            {
                CategoriesViewModel.Add(category);
            }

            foreach (var category in CategoriesViewModel)
            {
                if (ArticleViewModel.Category.Contains(category.Name))
                {
                    category.Selected = true;
                }
            }
        }

        protected async Task HandleValidEdit()
        {
            ArticleViewModel.UpdatedDate = DateTime.Now;

            foreach (var item in CategoriesViewModel)
            {
                if (item.Selected == true && !ArticleViewModel.Category.Contains(item.Name))
                {
                    ArticleViewModel.Category += item.Name;
                }

                if (item.Selected == false && ArticleViewModel.Category.Contains(item.Name))
                {
                    ArticleViewModel.Category = ArticleViewModel.Category.Replace(item.Name, string.Empty);
                }
            }

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

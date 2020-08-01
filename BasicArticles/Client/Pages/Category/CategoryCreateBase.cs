using BasicArticles.Client.ViewModels.Category;
using BasicArticles.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicArticles.Client.Pages.Category
{
    public class CategoryCreateBase : ComponentBase
    {
        [Inject]
        public ICategoryViewModel CategoryService { get; set; }
        [Inject]
        public NavigationManager Navigation { get; set; }

        public CategoryModel CategoryModel { get; set; } = new CategoryModel();
        public CategoryViewModel CategoryViewModel { get; set; } = new CategoryViewModel();

        protected void Cancel_Click()
        {
            Navigation.NavigateTo("manage");
        }

        protected async Task HandleValidCreate()
        {
            CategoryModel = CategoryViewModel;

            await CategoryService.CreateCategory(CategoryModel);

            Navigation.NavigateTo("manage");
        }
    }
}

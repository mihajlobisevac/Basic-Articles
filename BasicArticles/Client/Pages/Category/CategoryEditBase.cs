using BasicArticles.Client.ViewModels.Category;
using BasicArticles.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicArticles.Client.Pages.Category
{
    public class CategoryEditBase : ComponentBase
    {
        [Inject]
        protected ICategoryViewModel CategoryService { get; set; }
        [Inject]
        public NavigationManager Navigation { get; set; }

        [Parameter]
        public long Id { get; set; }
        public CategoryModel CategoryModel { get; set; } = new CategoryModel();
        public CategoryViewModel CategoryViewModel { get; set; } = new CategoryViewModel();

        protected override async Task OnInitializedAsync()
        {
            CategoryModel = await CategoryService.GetCategory(Id);

            CategoryViewModel = CategoryModel;
        }

        protected async Task HandleValidEdit()
        {
            CategoryModel = CategoryViewModel;

            await CategoryService.UpdateCategory(Id, CategoryModel);

            Navigation.NavigateTo("category/list");
        }

        protected void Cancel_Click()
        {
            Navigation.NavigateTo("category/list");
        }
    }
}

using BasicArticles.Client.ViewModels.Category;
using BasicArticles.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicArticles.Client.Pages.Category
{
    public class CategoryListBase : ComponentBase
    {
        [Inject]
        protected ICategoryViewModel CategoryService { get; set; }
        [Inject]
        protected NavigationManager Navigation { get; set; }

        public List<CategoryModel> CategoryList { get; set; } = new List<CategoryModel>();

        protected override async Task OnInitializedAsync()
        {
            CategoryList = await CategoryService.GetCategoryList();
        }

        protected void Navigate(string route)
        {
            Navigation.NavigateTo(route);
        }

        protected async Task DeleteCategory(long id)
        {
            await CategoryService.DeleteCategory(id);
            StateHasChanged();
            Navigation.NavigateTo("manage");
        }
    }
}

using BasicArticles.Client.ViewModels.Category;
using BasicArticles.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicArticles.Client.Shared
{
    public class HeaderNavBase : ComponentBase
    {
        [Inject]
        protected NavigationManager Navigation { get; set; }
        [Inject]
        protected ICategoryViewModel CategoryViewModel { get; set; }

        public List<CategoryModel> Categories { get; set; } = new List<CategoryModel>();

        protected override async Task OnInitializedAsync()
        {
            Categories = await CategoryViewModel.GetCategoryList();
        }
    }
}

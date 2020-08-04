using BasicArticles.Client.ViewModels.Article;
using BasicArticles.Client.ViewModels.Category;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace BasicArticles.Client.Pages.Article
{
    public class ArticleFormBase : ComponentBase
    {
        [Inject]
        public ICategoryViewModel CategoryService { get; set; }

        [Parameter]
        public ArticleViewModel Model { get; set; }
        [Parameter] 
        public List<CategoryViewModel> CategoriesViewModel { get; set; }
    }
}

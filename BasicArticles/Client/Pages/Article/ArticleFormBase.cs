using BasicArticles.Client.ViewModels.Article;
using BasicArticles.Client.ViewModels.Category;
using BasicArticles.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

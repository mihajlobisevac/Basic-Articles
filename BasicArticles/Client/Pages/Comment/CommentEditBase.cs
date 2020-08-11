using BasicArticles.Client.ViewModels.Comment;
using BasicArticles.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicArticles.Client.Pages.Comment
{
    public class CommentEditBase : ComponentBase
    {
        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject]
        private NavigationManager Navigation { get; set; }
        [Inject]
        private ICommentViewModel CommentService { get; set; }

        [Parameter]
        public long Id { get; set; }

        public CommentViewModel CommentViewModel { get; set; } = new CommentViewModel();
        public CommentModel CommentModel { get; set; } = new CommentModel();

        public string CommentAs { get; set; }

        protected override async Task OnInitializedAsync()
        {
            CommentViewModel = await CommentService.GetComment(Id);

            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            CommentAs = authState.User.Identity.Name;
        }

        protected async Task HandleValidComment()
        {
            CommentViewModel.UpdatedDate = DateTime.Now;

            CommentModel = CommentViewModel;

            await CommentService.UpdateComment(Id, CommentModel);

            Navigation.NavigateTo($"article/{CommentViewModel.Article}");
        }
    }
}

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
    public class CommentListBase : ComponentBase
    {
        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject]
        public NavigationManager Navigation { get; set; }
        [Inject]
        public ICommentViewModel CommentService { get; set; }

        public List<CommentModel> CommentList { get; set; } = new List<CommentModel>();

        public string User { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            User = authState.User.Identity.Name;

            CommentList = await CommentService.GetCommentListByUser(User);
        }

        protected void HandleEditComment(long comment)
        {
            Navigation.NavigateTo($"/comment/edit/{comment}");
        }
        protected async Task HandleDeleteComment(long comment)
        {
            await CommentService.DeleteComment(comment);

            CommentList = await CommentService.GetCommentListByUser(User);
        }
    }
}

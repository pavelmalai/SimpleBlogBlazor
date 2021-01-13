using Microsoft.AspNetCore.Components;
using SimpleBlogBlazor.Shared.Models;
using SimpleBlogBlazor.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBlogBlazor.UI.Pages.Posts
{
    public partial class Edit
    {
        public Post post { get; set; } = new Post();
        [Parameter]
        public Guid? PostId { get; set; }
        [Inject]
        private IPostsService postsService { get; set; }
        [Inject]
        private NavigationManager navigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (PostId.HasValue)
            {
                post = await postsService.GetPost(PostId.Value);
            }
            else
            {
                post = new Post();
            }
        }

        private async Task HandleValidSubmitAsync()
        {
            if (PostId.HasValue)
            {
                await postsService.UpdatePost(post);
            }
            else
            {
                post = await postsService.CreatePost(post);
            }

            navigationManager.NavigateTo($"/details/{post.PostId}");
        }

        private void HandleInvalidSubmit()
        {

        }

    }
}

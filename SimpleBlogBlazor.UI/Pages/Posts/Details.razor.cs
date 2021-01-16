using Microsoft.AspNetCore.Components;
using SimpleBlogBlazor.Shared.Models;
using SimpleBlogBlazor.UI.Services;
using System;
using System.Threading.Tasks;

namespace SimpleBlogBlazor.UI.Pages.Posts
{
    public partial class Details
    {
        [Parameter]
        public string PostId { get; set; }
        public Post Post { get; set; }
        [Inject]
        private IPostsService postsService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (PostId != null)
            {
                Post = await postsService.GetPost(Guid.Parse(PostId));
            }
        }
    }
}

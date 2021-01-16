using Microsoft.AspNetCore.Components;
using SimpleBlogBlazor.Shared.Models;
using SimpleBlogBlazor.UI.Services;
using System.Threading.Tasks;

namespace SimpleBlogBlazor.UI.Pages
{
    public partial class Index
    {
        public Post[] posts { get; set; }
        [Inject]
        public IPostsService postsService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            posts = (Post[])await postsService.GetAllPosts();
        }
    }
}
